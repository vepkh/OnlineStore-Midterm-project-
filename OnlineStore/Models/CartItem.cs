namespace OnlineStore.Models
{
    public class CartItem
    {
        // Product associated with this cart item
        public ProductClass ProductInCart { get; set; }

        // Quantity of the product in the cart
        public int Quantity { get; set; }

        // Constructor to initialize the properties
        public CartItem(ProductClass product, int quantity)
        {
            ProductInCart = product;
            Quantity = quantity;
        }
    }
}
