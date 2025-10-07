namespace Models
{
    public class Product
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public Product(int namber, string name, int price, int quantity)
        {
            Number = namber;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{Number}. {Name} - {Price} (Осталось: {Quantity})";
        }
    }
}