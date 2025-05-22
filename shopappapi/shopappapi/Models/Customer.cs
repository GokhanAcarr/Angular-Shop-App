namespace ShopAppApi.Models
{
  public class CustomerRequestDto
  {
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
  }

  public class CustomerDto
  {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; set; }
  }
}
