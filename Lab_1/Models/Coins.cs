namespace Models
{
    public class Coin
    {
        public int Denomination { get; set; }
        public int Count { get; set; }

        public Coin(int denomination, int count)
        {
            Denomination = denomination;
            Count = count;
        }

        public override string ToString()
        {
            return $"{Denomination} - {Count} шт.";
        }
    }
}