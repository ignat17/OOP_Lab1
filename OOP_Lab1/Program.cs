using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.Write("Введiть максимальну кiлькiсть продуктiв: ");
        string maxInput = Console.ReadLine();

        if (!int.TryParse(maxInput, out int maxCount) || maxCount <= 0)
            throw new Exception("Кiлькiсть повинна бути бiльшою за нуль.");

        List<Product> products = new List<Product>();

        while (true)
        {
            Console.WriteLine("\nМЕНЮ:");
            Console.WriteLine("1 - Додати продукт");
            Console.WriteLine("2 - Переглянути всi продукти");
            Console.WriteLine("3 - Знайти продукт");
            Console.WriteLine("4 - Демонстрацiя поведiнки");
            Console.WriteLine("5 - Видалити продукт");
            Console.WriteLine("0 - Вийти");
            Console.Write("Ваш вибiр: ");

            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        addproduct(products, maxCount);
                        break;

                    case "2":
                        viewall(products);
                        break;

                    case "3":
                        findproduct(products);
                        break;

                    case "4":
                        demonstrbehav(products);
                        break;

                    case "5":
                        deleteproduct(products);
                        break;

                    case "0":
                        return;

                    default:
                        throw new Exception("Невiрний пункт меню.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
        }
    }

    // ========================= ДОДАТИ ПРОДУКТ ==========================

    static void addproduct(List<Product> list, int max)
    {
        if (list.Count >= max)
            throw new Exception("Досягнуто максимальної кiлькостi продуктiв.");

        Console.WriteLine("1 - Ручне введення");
        Console.WriteLine("2 - Автоматичне створення");
        Console.Write("Ваш вибiр: ");

        string mode = Console.ReadLine();
        Product p = new Product();

        if (mode == "1")
        {
            p.Name = validname();
            p.Price = validprice();
            p.Stock = validstock();
            p.Category = validcategory();
        }
        else if (mode == "2")
        {
            Random rnd = new Random();
            p.Name = "Product_" + rnd.Next(100, 999);
            p.Price = rnd.Next(5, 500);
            p.Stock = rnd.Next(1, 100);
            p.Category = Category.Electronics;
        }
        else
            throw new Exception("Невiрний вибiр.");

        list.Add(p);
        Console.WriteLine("Продукт додано.");
    }

    // ========================= ВАЛІДАЦІЯ ==========================

    static string validname()
    {
        Console.Write("Введiть назву продукту: ");
        string input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
            throw new Exception("Назва не може бути порожньою.");

        if (input.Length < 3 || input.Length > 30)
            throw new Exception("Назва повинна мати 3–30 символiв.");

        return input;
    }

    static decimal validprice()
    {
        Console.Write("Введiть цiну: ");
        string input = Console.ReadLine();

        input = input.Replace('.', ','); // дозволяємо 12.50 і 12,50

        if (!decimal.TryParse(input, out decimal price))
            throw new Exception("Цiна повинна бути числом.");

        if (price < 0.01m || price > 100000m)
            throw new Exception("Цiна повинна бути в межах 0.01–100000.");

        return price;
    }

    static int validstock()
    {
        Console.Write("Введiть кiлькiсть на складi: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int stock))
            throw new Exception("Кiлькiсть повинна бути числом.");

        if (stock < 0 || stock > 10000)
            throw new Exception("Кiлькiсть повинна бути в межах 0–10000.");

        return stock;
    }

    static Category validcategory()
    {
        Console.Write("Введiть категорiю (Electronics, Clothes, Food, Furniture, Cosmetics): ");
        string input = Console.ReadLine();

        if (!Enum.TryParse(typeof(Category), input, true, out object result))
            throw new Exception("Такої категорiї не iснує.");

        return (Category)result;
    }

    // ========================= ПЕРЕГЛЯД ==========================

    static void viewall(List<Product> list)
    {
        if (list.Count == 0)
            throw new Exception("Список продуктiв порожнiй.");

        Console.WriteLine("\n#   | Назва                | Категорiя     |   Цiна    | Кiлькiсть");
        Console.WriteLine("-----------------------------------------------------------------------");

        int i = 1;
        foreach (var p in list)
        {
            Console.WriteLine(
                $"{i,-3} | " +
                $"{p.Name,-20} | " +
                $"{p.Category,-12} | " +
                $"{p.Price,9:F2} | " +
                $"{p.Stock,8}"
            );
            i++;
        }
    }

    // ========================= ПОШУК ==========================

    static void findproduct(List<Product> list)
    {
        if (list.Count == 0)
            throw new Exception("Немає продуктiв для пошуку.");

        Console.WriteLine("1 - Назва");
        Console.WriteLine("2 - Категорiя");
        Console.Write("Ваш вибiр: ");

        string choice = Console.ReadLine();
        List<Product> results = new List<Product>();

        if (choice == "1")
        {
            Console.Write("Введiть назву: ");
            string name = Console.ReadLine();

            foreach (var p in list)
                if (p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    results.Add(p);
        }
        else if (choice == "2")
        {
            Category cat = validcategory();

            foreach (var p in list)
                if (p.Category == cat)
                    results.Add(p);
        }
        else
            throw new Exception("Невiрний вибiр.");

        if (results.Count == 0)
        {
            Console.WriteLine("Не знайдено.");
            return;
        }

        Console.WriteLine("\n#   | Назва                | Категорiя     |   Цiна    | Кiлькiсть");
        Console.WriteLine("-----------------------------------------------------------------------");

        int n = 1;
        foreach (var p in results)
        {
            Console.WriteLine(
                $"{n,-3} | " +
                $"{p.Name,-20} | " +
                $"{p.Category,-12} | " +
                $"{p.Price,9:F2} | " +
                $"{p.Stock,8}"
            );
            n++;
        }
    }

    // ========================= ДЕМОНСТРАЦІЯ ПОВЕДІНКИ ==========================

    static void demonstrbehav(List<Product> list)
    {
        if (list.Count == 0)
            throw new Exception("Немає продуктiв для демонстрацiї.");

        viewall(list);
        Console.Write("Номер продукту: ");

        string input = Console.ReadLine();
        if (!int.TryParse(input, out int num) || num < 1 || num > list.Count)
            throw new Exception("Некоректний номер.");

        Product p = list[num - 1];

        Console.WriteLine("1 - Додати на склад");
        Console.WriteLine("2 - Придбати");
        Console.WriteLine("3 - Знижка");
        Console.Write("Ваш вибiр: ");

        string c = Console.ReadLine();

        switch (c)
        {
            case "1":
                Console.Write("Кiлькiсть: ");
                if (!int.TryParse(Console.ReadLine(), out int r1))
                    throw new Exception("Кiлькiсть повинна бути числом.");
                p.restock(r1);
                break;

            case "2":
                Console.Write("Кiлькiсть: ");
                if (!int.TryParse(Console.ReadLine(), out int r2))
                    throw new Exception("Кiлькiсть повинна бути числом.");
                p.purchase(r2);
                break;

            case "3":
                Console.Write("Вiдсоток: ");
                string dinput = Console.ReadLine();
                dinput = dinput.Replace('.', ',');
                if (!double.TryParse(dinput, out double d))
                    throw new Exception("Знижка повинна бути числом.");
                p.discount(d);
                break;

            default:
                throw new Exception("Невiрний вибiр.");
        }

        Console.WriteLine("Оновлено:");
        Console.WriteLine($"{p.Name} | {p.Category} | {p.Price} | {p.Stock}");
    }

    // ========================= ВИДАЛЕННЯ ==========================

    static void deleteproduct(List<Product> list)
    {
        if (list.Count == 0)
            throw new Exception("Немає продуктiв для видалення.");

        Console.WriteLine("1 - За номером");
        Console.WriteLine("2 - За категорiєю");
        Console.Write("Ваш вибiр: ");

        string ch = Console.ReadLine();

        if (ch == "1")
        {
            viewall(list);
            Console.Write("Номер: ");

            string input = Console.ReadLine();
            if (!int.TryParse(input, out int num) || num < 1 || num > list.Count)
                throw new Exception("Некоректний номер.");

            list.RemoveAt(num - 1);
            Console.WriteLine("Видалено.");
        }
        else if (ch == "2")
        {
            Category cat = validcategory();
            int removed = list.RemoveAll(p => p.Category == cat);
            Console.WriteLine($"Видалено: {removed}");
        }
        else
            throw new Exception("Некоректний вибiр.");
    }
}
