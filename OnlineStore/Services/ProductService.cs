
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Services
{
    public class ProductService
    {
        private readonly List<ProductClass> productslist; // Holds all products
        private readonly CategoryService categoryServiceobj;

        public ProductService(CategoryService catservice)
        {
            this.categoryServiceobj = catservice;
            productslist = new List<ProductClass>();
        }

        // Adds a new product to the list
        public ProductClass AddProduct(string name, string description, decimal price, int quantity, Guid categoryId, Guid productOwnerId)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < 1 || name.Length > 100 || description.Length > 4000 || price < 0 || quantity < 0)
                return null;

            // Check if the category exists
            var categoryofobj = categoryServiceobj.GetAllCategories().FirstOrDefault(c => c.Id == categoryId);
            if (categoryofobj == null)
            {
                Console.WriteLine("There is no such category ID");
                return null;
            }

            var productToAdd = new ProductClass(Guid.NewGuid(), name, description, price, quantity, categoryofobj, productOwnerId);
            productslist.Add(productToAdd);
            Console.WriteLine($"{name} added successfully");
            return productToAdd;
        }

        // Updates an existing product's details
        public ProductClass UpdateProduct(Guid productId, string name, string description, decimal price, int quantity, Guid categoryId)
        {
            var productToUpdate = productslist.FirstOrDefault(p => p.Id == productId);
            if (productToUpdate != null && price >= 0 && quantity >= 0)
            {
                var category = categoryServiceobj.GetAllCategories().FirstOrDefault(c => c.Id == categoryId);
                if (category == null)
                {
                    Console.WriteLine("Invalid category ID.");
                    return null;
                }

                // Update product details
                productToUpdate.Name = name;
                productToUpdate.Description = description;
                productToUpdate.Price = price;
                productToUpdate.AvailableQuantity = quantity;
                productToUpdate.categoryobj = category;
                Console.WriteLine("Product updated successfully");
                return productToUpdate;
            }

            Console.WriteLine("No such product found");
            return null;
        }

        // Deletes a product from the list
        public ProductClass DeleteProduct(string name)
        {
            var productToDelete = productslist.FirstOrDefault(p => p.Name == name);
            if (productToDelete != null)
            {
                productslist.Remove(productToDelete);
                Console.WriteLine($" {name} removed successfully");
                return productToDelete;
            }

            Console.WriteLine("No such product was found");
            return null;
        }
       
        // Get a list of all products
        public List<ProductClass> GetAllProducts()
        {
            return productslist;
        }

        // Get a specific product by ID
        public ProductClass GetProductById(Guid productId)
        {
            return productslist.FirstOrDefault(p => p.Id == productId);
        }
    }
}

