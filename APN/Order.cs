namespace APN;

public class Order
{
    private readonly List<OrderItem> _orderItems = new List<OrderItem>();

    public void AddProduct(Product product, int quantity = 1)
    {
        var existingProduct = _orderItems
            .FirstOrDefault(x => x.Product == product);
        if (existingProduct == null)
        {
            _orderItems.Add(new OrderItem(product, quantity));
        }
        else
        {
            existingProduct.Quantity += quantity;
        }
            
    }

    public void RemoveProduct(Product product, int quantity = 1)
    {
        var index = _orderItems.FindIndex(c => c.Product == product);
        if (_orderItems[index].Quantity > quantity)
        {
            _orderItems[index].Quantity -= quantity;
        }
        else if (_orderItems[index].Quantity == quantity)
        {
            _orderItems.RemoveAt(index);
        }
        else
        {
            throw new InvalidOperationException($"Cannot remove {quantity} products from {_orderItems[index].Product}");
        }
        
    }

    public decimal Total()
    {
        if (_orderItems.Count == 0)
        {
            return 0;
        }
        return _orderItems.Sum(x => x.CalculateItemPrice());
        
    }
}