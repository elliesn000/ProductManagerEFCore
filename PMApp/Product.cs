using System.ComponentModel.DataAnnotations;
namespace DbClasses;

public class Product
{    
    public int ProductId { get; set; }
    
    public string ProductName { get; set; } = "null";

    public decimal ProductPrice { get; set; }
    public int ProductQtt { get; set; }
    public int ProductStatus { get; set; } = 1; //1 = alive, 0 = deleted

    public List<Order> Order = new();

}