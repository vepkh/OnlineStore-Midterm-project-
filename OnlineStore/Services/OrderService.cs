/*using OnlineStore.Models;
using System;
using System.Linq;

namespace OnlineStore.Services
{
    public class OrderService
    {
        private readonly List<Order> orders;// stores all orders

        // Constructor to initialize the list of orders
        public OrderService()
        {
            orders = new List<Order>();
        }

        // Places an order for the user
        public bool PlaceOrder(User user)
        {
            if (user.Cart.AllProductsInCart.Count == 0)
                Console.WriteLine("Your cart is empty");
                return false;

            // Create the order
            var CurrentOrder = new Order(
                Guid.NewGuid(),
                user.Id,
                user.Cart.AllProductsInCart.First().ProductInCart.ProductOwnerId, // Executor based on first product
                DateTime.Now,
                user.Cart.AllProductsInCart,
                user.Cart.AllProductsInCart.Sum(p => p.ProductInCart.Price * p.Quantity),
                "Confirmed"
            );

            orders.Add(CurrentOrder);
            // Empty the user's cart after placing the order
            user.Cart.AllProductsInCart.Clear();
            Console.WriteLine("Your order request placed succesfully");
            return true;
        }

        // Cancels an order if its status is "Confirmed"
        public bool CancelOrder(Order order)
        {
            if (order.Status == "Confirmed")
            {
                order.Status = "Cancelled";
                Console.WriteLine($"You cancelled order with ID: {order.OrderId}");
                return true;
            }
            return false;
        }

        // Confirms an order (if stock is available and other conditions met)

        public bool ConfirmOrder(Order order)
        {
            if (order.Status == "Confirmed")
            {
                // Reduce product stock based on the order
                foreach (var item in order.ProductsInOrder)
                {
                    var product = new ProductService(new CategoryService()).GetProductById(item.ProductInCart.Id);
                    if (product != null && product.AvailableQuantity < item.Quantity)
                        return false; // Cannot confirm if stock is insufficient
                    product.AvailableQuantity -= item.Quantity;
                }

                order.Status = "Confirmed";
                return true;
            }
            return false;
        }

        // Completes an order after it's confirmed
        public bool CompleteOrder(Order order)
        {
            if (order.Status == "Confirmed")
            {
                order.Status = "Completed";
                Console.WriteLine($"Order with ID {order.OrderId} completed successfully");
                return true;
            }
            Console.WriteLine($"Oreder with ID {order.OrderId} is not copmleted yet");
            return false;
        }
    }
}
*/
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Services
{
    public class OrderService
    {
        private readonly List<Order> orders; // Stores all orders

        // Constructor to initialize the list of orders
        public OrderService()
        {
            orders = new List<Order>();
        }

        // Places an order for the user
        public Order PlaceOrder(User user)
        {
            if (user.Cart.AllProductsInCart.Count == 0)
            {
                Console.WriteLine("Your cart is empty");
                return null;
            }

            // Create the order
            var currentOrder = new Order(
                Guid.NewGuid(),
                user.Id,
                user.Cart.AllProductsInCart.First().ProductInCart.ProductOwnerId, // Executor based on the first product
                DateTime.Now,
                user.Cart.AllProductsInCart,
                user.Cart.AllProductsInCart.Sum(p => p.ProductInCart.Price * p.Quantity),
                "Confirmed"
            );

            orders.Add(currentOrder);

            // Empty the user's cart after placing the order
            user.Cart.AllProductsInCart.Clear();
            Console.WriteLine("Your order request has been placed successfully");

            return currentOrder;
        }

        // Cancels an order if its status is "Confirmed"
        public Order CancelOrder(Order order)
        {
            if (order.Status == "Confirmed")
            {
                order.Status = "Cancelled";
                Console.WriteLine($"You cancelled the order with ID: {order.OrderId}");
                return order;
            }

            Console.WriteLine($"Order with ID: {order.OrderId} could not be cancelled. Current status: {order.Status}");
            return null;
        }

        // Confirms an order (if stock is available and other conditions met)
        public Order ConfirmOrder(Order order)
        {
            if (order.Status == "Confirmed")
            {
                // Reduce product stock based on the order
                foreach (var item in order.ProductsInOrder)
                {
                    var product = new ProductService(new CategoryService()).GetProductById(item.ProductInCart.Id);
                    if (product != null && product.AvailableQuantity < item.Quantity)
                    {
                        Console.WriteLine($"Insufficient stock for product: {item.ProductInCart.Name}");
                        return null;
                    }

                    product.AvailableQuantity -= item.Quantity;
                }

                order.Status = "Confirmed";
                Console.WriteLine($"Order with ID: {order.OrderId} confirmed successfully");
                return order;
            }

            Console.WriteLine($"Order with ID: {order.OrderId} could not be confirmed. Current status: {order.Status}");
            return null;
        }

        // Completes an order after it's confirmed
        public Order CompleteOrder(Order order)
        {
            if (order.Status == "Confirmed")
            {
                order.Status = "Completed";
                Console.WriteLine($"Order with ID {order.OrderId} completed successfully");
                return order;
            }

            Console.WriteLine($"Order with ID {order.OrderId} could not be completed. Current status: {order.Status}");
            return null;
        }
    }
}

