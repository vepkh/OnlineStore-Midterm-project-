
using OnlineStore.Models;
using System;

using OnlineStore.Models;
using System;
using System.Linq;

namespace OnlineStore.Services
{
    public class CartService
    {
        private readonly ProductService productService;

        // Constructor to inject ProductService
        public CartService(ProductService productService)
        {
            this.productService = productService;
        }

        // Adds a product to the user's cart (increases quantity if already exists)
        public Cart AddToCart(Cart carriage, Guid productId, int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be more than 0");
                return carriage;
            }

            // Check if the product already exists in the cart
            var cartItem = carriage.AllProductsInCart.FirstOrDefault(p => p.ProductInCart.Id == productId);
            if (cartItem != null)
            {
                // Increase quantity of existing product in cart
                cartItem.Quantity += quantity;
            }
            else
            {
                // If not, add a new product to the cart
                var productToAdd = productService.GetProductById(productId); // Get the product details
                if (productToAdd == null)
                {
                    Console.WriteLine("No product with provided ID");
                    return carriage;
                }

                var newCartItem = new CartItem(productToAdd, quantity);
                carriage.AllProductsInCart.Add(newCartItem);
            }

            return carriage;
        }

        // Changes the quantity of a product in the user's cart
        public Cart ChangeQuantity(Cart carriage, Guid productId, int newQuantity)
        {
            var cartItem = carriage.AllProductsInCart.FirstOrDefault(p => p.ProductInCart.Id == productId);
            if (cartItem != null && newQuantity >= 0)
            {
                cartItem.Quantity = newQuantity;

                // If quantity is zero, remove it from the cart
                if (newQuantity == 0)
                {
                    carriage.AllProductsInCart.Remove(cartItem);
                }

                Console.WriteLine("Quantity changed successfully");
                return carriage;
            }

            Console.WriteLine("Make sure you entered a valid product ID and quantity.");
            return carriage;
        }

        // Removes a product from the user's cart
        public Cart RemoveFromCart(Cart carriage, Guid productId)
        {
            var cartItem = carriage.AllProductsInCart.FirstOrDefault(p => p.ProductInCart.Id == productId);
            if (cartItem != null)
            {
                carriage.AllProductsInCart.Remove(cartItem);
                Console.WriteLine($"Product with ID: {productId} removed successfully");
                return carriage;
            }

            Console.WriteLine("You entered an invalid product ID.");
            return carriage;
        }
    }
}

