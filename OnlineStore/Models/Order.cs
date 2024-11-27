namespace OnlineStore.Models
{
    public class Order
    {
        // Unique identifier for the order
        public Guid OrderId { get; set; }

        // Unique identifier for the customer who placed the order
        public Guid CustomerId { get; set; }

        // Unique identifier for the executor (product owner or seller)
        public Guid ExecutorId { get; set; }

        // Date and time when the order was placed
        public DateTime OrderDate { get; set; }

        // List of products included in the order along with their quantities
        public List<CartItem> ProductsInOrder { get; set; }

        // Total value of the order (calculated from product prices and quantities)
        public decimal TotalValue { get; set; }

        // Status of the order (e.g., Confirmed, Cancelled, Completed)
        public string Status { get; set; }

        // Constructor to initialize the properties
        public Order(Guid orderId, Guid customerId, Guid executorId, DateTime orderDate, List<CartItem> products, decimal totalValue, string status)
        {
            OrderId = orderId;
            CustomerId = customerId;
            ExecutorId = executorId;
            OrderDate = orderDate;
            ProductsInOrder = products;
            TotalValue = totalValue;
            Status = status;
        }
    }
}
