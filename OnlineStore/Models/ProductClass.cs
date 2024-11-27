namespace OnlineStore.Models
{
    public class ProductClass
    {
        
        public Guid Id { get; set; }

      
        public string Name { get; set; }

        
        public string Description { get; set; }

        
        public decimal Price { get; set; }

        public int AvailableQuantity { get; set; }

        
        public Category categoryobj { get; set; }

        //sellesr's id
        public Guid ProductOwnerId { get; set; }

       
        public ProductClass(Guid id, string name, string description, decimal price, int availableQuantity, Category category, Guid productOwnerId)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            AvailableQuantity = availableQuantity;
            categoryobj = category;
            ProductOwnerId = productOwnerId;
        }
    }
}
