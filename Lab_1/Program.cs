using System;
using Models;
using Services;


class Program
{
    static void Main()
    {
        VendingMachine machine = new VendingMachine();
        VendingMachineService service = new VendingMachineService(machine);


        while (true)
        {
            Console.WriteLine("=== ВЕНДИНГОВЫЙ АВТОМАТ ===");
            Console.WriteLine("\nВыберите режим:");
            Console.WriteLine("1 - Пользовательский режим");
            Console.WriteLine("2 - Администраторский режим");
            Console.WriteLine("3 - Выход");
            Console.Write("Ваш выбор: ");

            string mode = Console.ReadLine();

            if (mode == "1")
            {
                UserMode(service);
            }
            else if (mode == "2")
            {
                AdminMode(service);
            }
            else if (mode == "3")
            {
                Console.WriteLine("До свидания!");
                return;
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
        }
    }

    static void DelayTerminal()
    {
        Console.WriteLine("Нажмите любую клавишу, чтобы вернуться");
        Console.ReadKey();
        Console.WriteLine();
    }

    static void UserMode(VendingMachineService service)
    {
        while (true)
        {
            Console.WriteLine("\n=== ПОЛЬЗОВАТЕЛЬСКИЙ РЕЖИМ ===");
            Console.WriteLine($"Текущий баланс: {service.GetCurrentBalance()}");
            Console.WriteLine("1 - Посмотреть товары");
            Console.WriteLine("2 - Внести монеты");
            Console.WriteLine("3 - Купить товар");
            Console.WriteLine("4 - Вернуть деньги");
            Console.WriteLine("5 - Назад");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                DisplayProducts(service);
            }
            else if (choice == "2")
            {
                InsertCoins(service);
            }
            else if (choice == "3")
            {
                TryBuyProduct(service);
            }
            else if (choice == "4")
            {
                ReturnMoney(service);
            }
            else if (choice == "5")
            {
                return;
            }
            else
            {
                Console.WriteLine("Неверный выбор.");
            }
        }
    }

    static void DisplayProducts(VendingMachineService service)
    {
        service.DisplayProducts();
        DelayTerminal();
    }

    static void InsertCoins(VendingMachineService service)
    {
        Console.WriteLine($"Доступные номиналы: {string.Join(", ", service.GetAvailableDenominations())}");
        Console.Write("Введите номинал валюты: ");

        if (int.TryParse(Console.ReadLine(), out int denomination))
        {
            service.InsertCoin(denomination);
        }
        else
        {
            Console.WriteLine("Неверный формат номинала.");
        }
        DelayTerminal();
    }

    static void TryBuyProduct(VendingMachineService service)
    {
        service.DisplayProducts();
        Console.Write("Введите номер товара: ");
        if (int.TryParse(Console.ReadLine(), out int productNum) && productNum >= 0)
        {
            service.BuyProduct(productNum);
        }
        else
        {
            Console.WriteLine("Неверный формат номера.");
        }
        DelayTerminal();
    }

    static void ReturnMoney(VendingMachineService service)
    {
        Console.WriteLine("Происходит возврат средств: ");
        service.ReturnMoney();
        DelayTerminal();
    }

    static void AdminMode(VendingMachineService service)
    {
        const string adminPassword = "admin123";

        Console.Write("Введите пароль администратора: ");
        string password = Console.ReadLine();

        if (password != adminPassword)
        {
            Console.WriteLine("Неверный пароль.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n=== АДМИНИСТРАТОРСКИЙ РЕЖИМ ===");
            Console.WriteLine("1 - Добавить новый товар");
            Console.WriteLine("2 - Пополнить товар");
            Console.WriteLine("3 - Собрать средства");
            Console.WriteLine("4 - Просмотреть статистику");
            Console.WriteLine("5 - Назад");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                AddNewProduct(service);
            }
            else if (choice == "2")
            {
                RefillProduct(service);
            }
            else if (choice == "3")
            {
                TakeMoney(service);
            }
            else if (choice == "4")
            {
                DisplayStatistics(service);
            }
            else if (choice == "5")
            {
                return;
            }
            else
            {
                Console.WriteLine("Неверный формат команды.");
            }
        }
    }

    static void AddNewProduct(VendingMachineService service)
    {
        Console.Write("Введите номер товара: ");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 0)
        {
            Console.Write("Введите название товара: ");
            string name = Console.ReadLine();

            Console.Write("Введите цену товара: ");
            if (int.TryParse(Console.ReadLine(), out int price) && price > 0)
            {
                Console.Write("Введите количество: ");
                if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                {
                    service.AddNewProduct(number, name, price, quantity);
                }
                else
                {
                    Console.WriteLine("Неверный формат количества.");
                }
            }
            else
            {
                Console.WriteLine("Неверный формат цены.");
            }
        }
        else
        {
            Console.WriteLine("Неверный формат номера.");
        }
        DelayTerminal();
    }

    static void RefillProduct(VendingMachineService service)
    {
        Console.Write("Введите номер товара: ");
        if (int.TryParse(Console.ReadLine(), out int number) && number >= 0)
        {
            Console.Write("Введите количество добавляемого товара: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            {
                service.RefillProduct(number, quantity);
            }
            else
            {
                Console.WriteLine("Неверный формат количества");
            }
        }
        else
        {
            Console.WriteLine("Неверный формат номера");
        }
        DelayTerminal();
    }

    static void TakeMoney(VendingMachineService service)
    {
        Console.WriteLine("Доступные монеты: ");
        service.DisplayCoins();
        Console.WriteLine("Выберите номинал: ");
        if (int.TryParse(Console.ReadLine(), out int denomination))
        {
            Console.WriteLine("Введите количество валюты: ");
            if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
            {
                service.TakeMoney(denomination, quantity);
            }
            else
            {
                Console.WriteLine("Неверный формат количества");
            }
        }
        else
        {
            Console.WriteLine("Неверный формат номинала");
        }
        DelayTerminal();
    }

    static void DisplayStatistics(VendingMachineService service)
    {
        service.DisplayStatistics();
        DelayTerminal();
    }
}
