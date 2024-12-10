namespace APN;

public class OrderItem
{
    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; set; }
    public int Quantity { get; set; }
    

    public decimal CalculateItemPrice() => Product.Price * Quantity;
    

    public string FormattedOrderItem()
    {
        return $"Product: {Product.ProductName}, Quantity: {Quantity}";
    }
}