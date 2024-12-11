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

    public void RemoveProduct(string name, int quantity = 1)
    {
        var orderItem = _orderItems
            .FirstOrDefault(i => string.Equals(i.Product.ProductName, name, StringComparison.OrdinalIgnoreCase));

        if (orderItem == null)
        {
            throw new InvalidOperationException($"Product with name '{name}' not found in the order.");
        }

        RemoveOrderItem(orderItem, quantity);
    }

    private void RemoveOrderItem(OrderItem orderItem, int quantity = 1)
    {
        if (orderItem.Quantity > quantity)
        {
            orderItem.Quantity -= quantity;
        }
        else if (orderItem.Quantity == quantity)
        {
            _orderItems.Remove(orderItem);
        }
        else
        {
            throw new InvalidOperationException($"Cannot remove {quantity} products. Only {orderItem.Quantity} available.");
        }
    }

    public void DisplayOrderSummary()
    {
        foreach (var orderItem in _orderItems)
        {
            Console.WriteLine(orderItem.FormattedOrderItem());
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