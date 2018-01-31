using System;
using System.Collections.Generic;
using Xunit;

namespace EventSourcedTooling.Tests
{
    public class ShoppingCartTests
    {
        [Fact]
        public void StartShopping()
        {
            new Specification<StartShopping>(new ShoppingCartAggregate())
                .When(new StartShopping("customerId", "cartId", "startTime"))
                .Then(new CustomerStartedShopping("customerId", "cartId"));
        }

        [Fact]
        public void AddPoductToCart()
        {
            new Specification<AddProductToCart>(new ShoppingCartAggregate())
                .Given(new CustomerStartedShopping("customerId", "cartId"))
                .When(new AddProductToCart("cartId", "SKU", "price", "addTime"))
                .Then(new ProductWasAddedToCart("customerId", "cartId", "SKU", "price", "addTime"));
        }

        [Fact]
        public void AddProductWithoutCart()
        {
            Assert.Throws<Exception>(() => 
                new Specification<AddProductToCart>(new ShoppingCartAggregate())
                    .When(new AddProductToCart("cartId", "SKU", "price", "addTime")));
        }
        
        [Fact]
        public void PlaceEmptyOrder()
        {
            new Specification<PlaceOrder>(new ShoppingCartAggregate())
                .Given(new CustomerStartedShopping("customerId", "cartId"))
                .When(new PlaceOrder("cartId", "customerId", "orderTime"))
                .Then(new CustomerPlacedOrder("customerId", "cartId", new List<Product>(), "orderTime"));
        }
        
        [Fact]
        public void PlaceOneProductOrder()
        {
            new Specification<PlaceOrder>(new ShoppingCartAggregate())
                .Given(
                    new CustomerStartedShopping("customerId", "cartId"), 
                    new ProductWasAddedToCart("customerId", "cartId", "SKU", "price", "addTime"))
                .When(new PlaceOrder("cartId", "customerId", "orderTime"))
                .Then(new CustomerPlacedOrder("customerId", "cartId", new List<Product>{new Product("SKU", "1", "price", "currency")}, "orderTime"));
        }

    }

}