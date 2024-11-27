namespace OnlineStore.Models
{
    public class Cart
    {
        // List of products in the user's shopping cart
        public List<CartItem> AllProductsInCart { get; set; }

        // Constructor to initialize the products list
        public Cart()
        {
            AllProductsInCart = new List<CartItem>();
        }
    }
}
