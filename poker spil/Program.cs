namespace poker_spil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, fugfytu!");
        }
    }
    public class Player
    {
        Card[] cards = new Card[2];
    }
    public class Card
    {
        int number;
        string suit;
        public Card(int number, string suit)
        {
            this.number = number;
            this.suit = suit;
        }
    }
    public class GameLogic
    {

    }
}