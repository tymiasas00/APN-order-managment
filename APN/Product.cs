namespace APN;

public class Product
{
    private string ProductName { get; set; }
    private decimal Price { get; set; }

    public Product(string productName, decimal price)
    {
        ProductName = productName;
        Price = price;
    }
    
}