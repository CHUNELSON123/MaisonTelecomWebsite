namespace MaisonTelecom.Services
{
    public class BasketState
    {
        // A simple list to hold items in the cart
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }

    public class BasketItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}