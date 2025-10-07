using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class VendingMachineService
    {
        private VendingMachine _machine;

        public VendingMachineService(VendingMachine machine)
        {
            _machine = machine;
        }

        public int GetCurrentBalance()
        {
            return _machine.CurrentBalance;
        }

        public List<int> GetAvailableDenominations()
        {
            return _machine.Coins.Select(c => c.Denomination).Order().ToList();
        }

        public void DisplayProducts()
        {
            Console.WriteLine("\n=== ДОСТУПНЫЕ ТОВАРЫ ===");
            foreach (Product product in _machine.Products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine("========================");
        }

        public void InsertCoin(int amount)
        {
            Coin coin = _machine.Coins.FirstOrDefault(c => c.Denomination == amount);
            if (coin != null)
            {
                coin.Count++;
                _machine.CurrentBalance += amount;
                Console.WriteLine($"Внесено: {amount}. Текущий баланс: {_machine.CurrentBalance}");
            }
            else
            {
                Console.WriteLine("Монета такого номинала не принимается.");
            }
        }

        public void BuyProduct(int productNum)
        {
            Product product = _machine.Products.FirstOrDefault(p => p.Number == productNum);
            
            if (product == null)
            {
                Console.WriteLine("Товар не найден.");
                return;
            }

            if (product.Quantity <= 0)
            {
                Console.WriteLine("Товар закончился.");
                return;
            }

            if (_machine.CurrentBalance < product.Price)
            {
                Console.WriteLine($"Недостаточно средств. Нужно: {product.Price}, внесено: {_machine.CurrentBalance}");
                return;
            }

            product.Quantity--;
            _machine.CollectedMoney += product.Price;
            int change = _machine.CurrentBalance - product.Price;
            _machine.CurrentBalance = 0;

            Console.WriteLine($"Вы купили: {product.Name}");
            if (change > 0)
            {
                Console.WriteLine($"Сдача: {change}");
                GiveChange(change);
            }
        }

        private void GiveChange(int amount)
        {
            Dictionary<int, int> coinsToGive = new Dictionary<int, int>();
            int remaining = amount;

            foreach (Coin coin in _machine.Coins.OrderByDescending(c => c.Denomination))
            {
                if (coin.Count > 0 && remaining >= coin.Denomination)
                {
                    int count = (int)(remaining / coin.Denomination);
                    count = Math.Min(count, coin.Count);
                    coinsToGive.Add(coin.Denomination, count);
                    remaining -= count * coin.Denomination;
                    coin.Count -= count;
                }
            }

            if (remaining > 0)
            {
                Console.WriteLine($"Нехватило денег, будет выдано только {amount - remaining}");
            }

            foreach (var coin in coinsToGive)
            {
                Console.Write($"{coin.Key} P x {coin.Value}; ");
            }
            Console.WriteLine();
        }

        public void ReturnMoney()
        {
            if (_machine.CurrentBalance > 0)
            {
                GiveChange(_machine.CurrentBalance);
                _machine.CurrentBalance = 0;
            }
            else
            {
                Console.WriteLine("Баланс пуст.");
            }
        }

        public void AddNewProduct(int number, string name, int price, int quantity)
        {
            if (_machine.Products.FirstOrDefault(p => p.Number == number) != null)
            {
                Console.WriteLine($"Товар с номером {number} уже существует");
                return;
            }
            _machine.Products.Add(new Product(number, name, price, quantity));
            Console.WriteLine($"Товар '{name}' добавлен.");
        }

        public void RefillProduct(int number, int quantity)
        {
            Product product = _machine.Products.FirstOrDefault(p => p.Number == number);
            if (product == null)
            {
                Console.WriteLine($"Товар с номером {number} не найден");
                return;
            }
            product.Quantity += quantity;
            Console.WriteLine($"Товар '{product.Name}' пополнен.");
        }

        public void TakeMoney(int denomination, int count)
        {
            Coin coin = _machine.Coins.FirstOrDefault(c => c.Denomination == denomination);
            if (coin == null)
            {
                Console.WriteLine($"Валюты номиналом {denomination} не найдено.");
                return;
            }
            if (coin.Count < count)
            {
                Console.WriteLine($"Недостаточно валюты {denomination}.");
                return;
            }
            coin.Count -= count;
        }

        public void DisplayCoins()
        {
            foreach (Coin coin in _machine.Coins)
            {
                Console.WriteLine(coin);
            }
        }

        public void DisplayStatistics()
        {
            Console.WriteLine("\n=== СТАТИСТИКА ===");
            Console.WriteLine($"Собрано средств: {_machine.CollectedMoney}");
            Console.WriteLine("Остатки монет:");
            DisplayCoins();
            Console.WriteLine("==================");
        }
    }
}