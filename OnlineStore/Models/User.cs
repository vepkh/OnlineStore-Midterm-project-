namespace OnlineStore.Models
{
    public class User
    {
        // Unique identifier for the user
        public Guid Id { get; set; }

        // Name of the user (e.g., John Doe)
        public string Name { get; set; }

        // Email address of the user
        public string Email { get; set; }

        // User's password 
        public string Password { get; set; }

        // List of orders placed by the user
        public List<Order> Orders { get; set; }

        // Shopping cart associated with the user
        public Cart Cart { get; set; }

        // Constructor to initialize the properties
        public User(Guid id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Orders = new List<Order>();
            Cart = new Cart(); // Initialize an empty cart for the user
        }
    }
}
