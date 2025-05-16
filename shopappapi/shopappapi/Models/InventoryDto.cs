namespace shopappapi.Models
{
  public class InventoryDto
  {
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int AvailableQty { get; set; }
    public int ReOrderPoint { get; set; }
  }
}
