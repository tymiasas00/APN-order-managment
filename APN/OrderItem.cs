namespace APN;

public class OrderItem(Product product, int quantity)
{
    public Product Product { get; set; } = product;
    public int Quantity { get; set; } = quantity;


    public decimal CalculateItemPrice() => Product.Price * Quantity;
    

    public string FormattedOrderItem()
    {
        return $"Produkt: {Product}, Ilość: {Quantity}";
    }
}