using MaisonTelecom.Models;

public class WishlistItem
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string SessionId { get; set; }
}