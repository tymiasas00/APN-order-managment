namespace APN;

public class Product(string productName, decimal price)
{
    public string ProductName { get; set; } = productName;
    public decimal Price { get; set; } = price;
    
    public override string ToString() => $"{ProductName}: {Price}zł";
}