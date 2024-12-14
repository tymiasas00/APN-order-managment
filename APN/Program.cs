namespace APN;

internal class Program
{
    static void Main()
    {
        var products = new Dictionary<int, Product>
        {
            { 1, new Product("Laptop", 2500) },
            { 2, new Product("Klawiatura", 120) },
            { 3, new Product("Mysz", 90) },
            { 4, new Product("Monitor", 1000) },
            { 5, new Product("Kaczka debuggująca", 66) }
        };

        var order = new Order();
        bool running = true;

        while (running)
        {
            PrintSeparator();
            Console.WriteLine("            SYSTEM ZARZĄDZANIA ZAMÓWIENIAMI");
            PrintSeparator();
            Console.WriteLine("Wybierz opcję:");
            Console.WriteLine("1. Dodaj produkt");
            Console.WriteLine("2. Usuń produkt");
            Console.WriteLine("3. Wyświetl wartość zamówienia");
            Console.WriteLine("4. Wyjście");
            PrintSeparator();

            Console.Write("Twój wybór: ");
            var choice = int.TryParse(Console.ReadLine()?.Trim(), out int parsedChoice) ? parsedChoice : 0;

            if (choice == 0)
            {
                Console.WriteLine("\n[!] Wybrano niepoprawną opcję. Spróbuj ponownie.\n");
                continue;
            }

            switch (choice)
            {
                case 1:
                    PrintSeparator();
                    Console.WriteLine("DOSTĘPNE PRODUKTY:");
                    PrintSeparator();

                    foreach (var kvp in products)
                    {
                        Console.WriteLine($"{kvp.Key}. {kvp.Value.ProductName} - {kvp.Value.Price:C}");
                    }

                    Console.Write("\nWybierz numer produktu: ");
                    if (int.TryParse(Console.ReadLine(), out int productNumber) && products.ContainsKey(productNumber))
                    {
                        Console.Write("Podaj ilość: ");
                        if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                        {
                            order.AddProduct(products[productNumber], quantity);
                            Console.WriteLine("\n[+] Produkt dodany do zamówienia.\n");
                        }
                        else
                        {
                            Console.WriteLine("\n[!] Nieprawidłowa ilość.\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("\n[!] Nieprawidłowy numer produktu.\n");
                    }
                    break;

                case 2:
                    PrintSeparator();
                    Console.WriteLine("USUWANIE PRODUKTU:");
                    PrintSeparator();

                    Console.Write("Podaj nazwę produktu do usunięcia: ");
                    string? productName = Console.ReadLine();

                    Console.Write("Podaj ilość do usunięcia (domyślnie 1): ");
                    if (!int.TryParse(Console.ReadLine(), out int removeQuantity) || removeQuantity <= 0)
                    {
                        Console.WriteLine("\n[!] Nieprawidłowa ilość. Ustawiono ilość domyślną: 1.\n");
                        removeQuantity = 1;
                    }

                    try
                    {
                        order.RemoveProduct(productName, removeQuantity);
                        Console.WriteLine("\n[-] Produkt został usunięty lub zredukowano ilość.\n");
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"[!] Błąd: {ex.Message}\n");
                    }
                    break;

                case 3:
                    PrintSeparator();
                    Console.WriteLine("PODSUMOWANIE ZAMÓWIENIA:");
                    PrintSeparator();

                    if (order.Total() == 0)
                    {
                        Console.WriteLine("Zamówienie jest puste.\n");
                    }
                    else
                    {
                        order.DisplayOrderSummary();
                        Console.WriteLine($"\nŁączna wartość zamówienia: {order.Total():C}\n");
                    }
                    break;

                case 4:
                    running = false;
                    Console.WriteLine("\nDziękujemy za skorzystanie z aplikacji. Do zobaczenia!\n");
                    break;

                default:
                    Console.WriteLine("\n[!] Nieprawidłowy wybór. Spróbuj ponownie.\n");
                    break;
            }
        }
    }

    private static void PrintSeparator()
    {
        Console.WriteLine(new string('-', 50));
    }
}
