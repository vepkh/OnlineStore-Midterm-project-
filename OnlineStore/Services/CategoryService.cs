using OnlineStore.Models;
using System;
using System.Linq;

namespace OnlineStore.Services
{
    public class CategoryService
    {
        private readonly List<Category> categoriebislisti;

        // Constructor to initialize the list of categories
        public CategoryService()
        {
            categoriebislisti = new List<Category>();
        }

        public Category AddCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            {
                throw new ArgumentException("Category name must be between 1 and 100 characters.");
            }

            var newCategory = new Category(Guid.NewGuid(), name);
          //  {
            //    Id = Guid.NewGuid();
               // Name = name;
            //};

            categoriebislisti.Add(newCategory);
            return newCategory; // Return the created category
        }

        // Updates an existing category's name and returns the updated Category
        public Category UpdateCategory(Guid categoryId, string updatedName)
        {
            var category = categoriebislisti.FirstOrDefault(c => c.Id == categoryId); // Find the category by ID
            if (category != null && updatedName.Length >= 1 && updatedName.Length <= 100)
            {
                category.Name = updatedName;
                Console.WriteLine($"Category updated to {updatedName}");
                return category; // Return the updated category
            }
            return null; // Return null if update fails
        }

        // Updates an existing category's name
        /* public bool UpdateCategory(Guid categoryId, string updatedname)
         {
             var category = categoriebislisti.FirstOrDefault(cobj => cobj.Id == categoryId);//searches icaregory with id provided
             if (category != null && updatedname.Length >= 1 && updatedname.Length <= 100)
             {
                 category.Name = updatedname;
                 return true;
             }
             return false;
         }*/

        public List<Category> GetAllCategories()
        {
            return categoriebislisti;
        }
    }
 }
