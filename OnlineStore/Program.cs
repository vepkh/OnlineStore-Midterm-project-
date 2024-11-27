using OnlineStore.Models;
using OnlineStore.Services;
using System;

namespace OnlineStore
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize services
            var categoryService = new CategoryService();
            var productService = new ProductService(categoryService);
            var cartService = new CartService(productService);
            var userService = new UserService();
            var orderService = new OrderService();

            // Add categories
            var electronicsCategory = categoryService.AddCategory("Electronics");
            var toysCategory = categoryService.AddCategory("Toys");
            var booksCategory = categoryService.AddCategory("Books");

            User user = null; // Store the registered user

            try
            {
                // Registering a user and retrieving the User object
                user = userService.RegisterUser("JohnDoe", "john.doe@gmail.com", "password123");
                Console.WriteLine($"User {user.Name} registered successfully.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error during user registration: {ex.Message}");
                return; // Exit if user registration fails
            }

            // Add products
            var products = new[]
            {
                productService.AddProduct("iPhone 15", "Latest iPhone", 3000.99m, 1000, electronicsCategory.Id, user.Id),
                productService.AddProduct("Teddy Bear", "Soft and cuddly toy", 30.99m, 100, toysCategory.Id, user.Id),
                productService.AddProduct("Fiction Novel", "Bestselling fiction book", 15.99m, 200, booksCategory.Id, user.Id),
                productService.AddProduct("Samsung Galaxy S23", "For Android enjoyers", 2499.99m, 1500, electronicsCategory.Id, user.Id)
            };

            Console.WriteLine("\nAvailable Products:");
            for (int i = 0; i < products.Length; i++)
            {
                var product = products[i];
                Console.WriteLine($"{i + 1}. {product.Name} - {product.Price:C} ({product.AvailableQuantity} available)");
            }

            // Add products to cart
            Console.WriteLine("\nPlease select products to add to your cart:");
            bool addingToCart = true;
            while (addingToCart)
            {
                Console.Write("Enter product number (or 0 to stop): ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= products.Length)
                {
                    var selectedProduct = products[choice - 1];
                    Console.Write($"Enter quantity for {selectedProduct.Name} (Available: {selectedProduct.AvailableQuantity}): ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        if (quantity <= selectedProduct.AvailableQuantity)
                        {
                            user.Cart = cartService.AddToCart(user.Cart, selectedProduct.Id, quantity);
                            selectedProduct.AvailableQuantity -= quantity; // Subtract the entered quantity from available quantity
                            Console.WriteLine($"{quantity} x {selectedProduct.Name} added to cart.");
                        }
                        else
                        {
                            Console.WriteLine($"Invalid quantity. Only {selectedProduct.AvailableQuantity} available.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity. Try again.");
                    }
                }
                else if (choice == 0)
                {
                    addingToCart = false;
                }
                else
                {
                    Console.WriteLine("Invalid product number. Try again.");
                }
            }

            // Display cart contents
            Console.WriteLine("\nCart Contents:");
            if (user.Cart.AllProductsInCart.Count > 0)
            {
                foreach (var cartItem in user.Cart.AllProductsInCart)
                {
                    Console.WriteLine($"Product: {cartItem.ProductInCart.Name}, Quantity: {cartItem.Quantity}, Total Price: {cartItem.ProductInCart.Price * cartItem.Quantity:C}");
                }
            }
            else
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            // Place an order
            Console.WriteLine("\nPlacing order...");
            var order = orderService.PlaceOrder(user);

            if (order != null)
            {
                Console.WriteLine($"\nOrder placed successfully with ID: {order.OrderId}");
                Console.WriteLine($"Total Amount: {order.TotalValue:C}");
                Console.WriteLine($"Order Status: {order.Status}");

                // Completing the order
                Console.WriteLine("\nCompleting the order...");
                var isOrderCompleted = orderService.CompleteOrder(order);

                if (isOrderCompleted != null && isOrderCompleted.Status == "Completed")
                {
                    Console.WriteLine($"Order with ID: {order.OrderId} completed successfully!");
                }
                else
                {
                    Console.WriteLine($"Order with ID: {order.OrderId} could not be completed.");
                }
            }
            else
            {
                Console.WriteLine("Failed to place the order.");
            }
        }
    }
}
