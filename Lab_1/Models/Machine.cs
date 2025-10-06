using System.Collections.Generic;

namespace Models
{
    public class VendingMachine
    {
        public List<Product> Products { get; set; }
        public List<Coin> Coins { get; set; }
        public int CurrentBalance { get; set; }
        public int CollectedMoney { get; set; }

        public VendingMachine()
        {
            Products = new List<Product>();
            Coins = new List<Coin>();
            CurrentBalance = 0;
            CollectedMoney = 0;
            
            // Инициализация монет
            InitializeCoins();
            // Инициализация товаров
            InitializeProducts();
        }

        private void InitializeCoins()
        {
            Coins.Add(new Coin(1, 10));
            Coins.Add(new Coin(2, 10));
            Coins.Add(new Coin(5, 10));
            Coins.Add(new Coin(10, 10));
            Coins.Add(new Coin(50, 10));
        }

        private void InitializeProducts()
        {
            Products.Add(new Product(1, "Вода", 40, 5));
            Products.Add(new Product(2, "Шоколад", 90, 3));
            Products.Add(new Product(3, "Чипсы", 105, 4));
            Products.Add(new Product(4, "Печенье", 75, 6));
        }
    }
}