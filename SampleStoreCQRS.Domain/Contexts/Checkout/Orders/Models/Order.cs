﻿using System;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Enuns;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Events;
using SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Validations.Models;
using SampleStoreCQRS.Domain.Core.Models;
using SampleStoreCQRS.Domain.Core.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using SampleStoreCQRS.Domain.Core.Interfaces;

namespace SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models
{
    public class Order : Aggregate
    {

        private ICollection<OrderItem> _items;

        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public EOrderStatus Status { get; protected set; }
        public string Number { get; protected set; }
        public DateTime CreateAt { get; protected set; }
        public DateTime UpdateAt { get; protected set; }
        public Customer Customer { get; protected set; }
        public Payment Payment { get; protected set; }
        public DiscountCupon DiscountCupon { get; protected set; }
        public decimal Total { get { return _items.Sum(x => x.Price); } }
        public decimal TotalWithDiscount { get; protected set; }

        protected Order() { }

        protected Order(
            Customer customer,
            Payment payment)
        {
            Customer = customer;
            _items = new List<OrderItem>();
            Status = EOrderStatus.Created;
            Payment = payment;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;

            ValidationResult = new OrderValidation().Validate(this);
        }

        // add an item
        public void AddItem(Product product, decimal quantity)
        {

            var item = new OrderItem(product, quantity);
            AddNotifications(item.ValidationResult.Errors);

            if (ValidationResult.IsValid)
            {
                _items.Add(item);
            }
        }

        // place 
        public void Place()
        {

            if (_items.Count == 0)
            {
                AddNotification("Este pedido não possui itens");
                return;
            }

            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8).ToUpper();

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer, Status, Number));
            AddEvent(new OrderPlacedEvent(Id, Customer, Payment, Number));
        }

        // pay
        public void Pay()
        {
            if (Status == EOrderStatus.Canceled)
            {
                AddNotification("Este pedido não pode ser pago pois está cancelado");
                return;
            }

            Status = EOrderStatus.Paid;

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer, Status, Number));
        }

        // ship
        public void Ship()
        {

            if (Status != EOrderStatus.Paid)
            {
                AddNotification("Este pedido não pode ser entregue pois o pagamento não foi processado");
                return;
            }

            if (Status == EOrderStatus.Canceled)
            {
                AddNotification("Este pedido não pode ser entregue pois está cancelado");
                return;
            }

            Status = EOrderStatus.Shipped;

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer, Status, Number));
        }

        // canceled
        public void Cancel()
        {

            if (Status == EOrderStatus.Shipped)
            {
                AddNotification("Este pedido não pode ser cancelado pois já foi entregue");
                return;
            }

            Status = EOrderStatus.Canceled;

            // domain events
            AddEvent(new OrderStatusChangedEvent(Id, Customer, Status, Number));
        }

        // apply discount
        public Order ApplyDiscount(DiscountCupon cupon)
        {
            DiscountCupon = cupon;
            TotalWithDiscount = Total - (Total * (cupon.Percentage / 100));

            AddEvent(new AppliedDiscountEvent(Id, Customer.Id, DiscountCupon.Cod, DiscountCupon.Percentage, Total, TotalWithDiscount));
            
            return this;
        }

        public override bool IsValid()
        {
            return ValidationResult.IsValid;
        }

        public static class Factory
        {
            public static Order Create(Customer customer, IPaymentMethod paymentMethod)
            {
                return new Order(customer, new Payment(paymentMethod));
            }
        }
    }
}
