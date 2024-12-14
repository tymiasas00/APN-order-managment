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

    public void RemoveProduct(string? name, int quantity = 1)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("[!] Nazwa produktu nie może być pusta.");
            return;
        }
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

        decimal discount = CalculateProductDiscount();
        decimal totalBeforeDiscount = _orderItems.Sum(x => x.CalculateItemPrice());

        Console.WriteLine($"\nWartość zamówienia przed rabatem: {totalBeforeDiscount:C}");
        if (discount > 0)
        {
            Console.WriteLine($"Rabat na produkty: -{discount:C}");
        }

        decimal total = Total();
        if (totalBeforeDiscount > 5000)
        {
            Console.WriteLine($"Rabat 5% za przekroczenie 5000 PLN: -{totalBeforeDiscount * 0.05m:C}");
        }
        Console.WriteLine($"Łączna wartość zamówienia: {total:C}\n");
    }

    public decimal Total()
    {
        if (_orderItems.Count == 0)
        {
            return 0;
        }

        decimal total = _orderItems.Sum(x => x.CalculateItemPrice());

        total -= CalculateProductDiscount();

        if (total > 5000)
        {
            total *= 0.95m; 
        }

        return total;
    }

    private decimal CalculateProductDiscount()
    {
        var allProducts = _orderItems
            .SelectMany(x => Enumerable.Repeat(x.Product.Price, x.Quantity))
            .OrderBy(price => price)
            .ToList();

        if (allProducts.Count < 2)
        {
            return 0; 
        }

        decimal discountOption1 = allProducts[1] * 0.10m;

        decimal discountOption2 = allProducts.Count >= 3 ? allProducts[2] * 0.20m : 0;

        return Math.Max(discountOption1, discountOption2);
    }
}