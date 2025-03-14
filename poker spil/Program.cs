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
        int suit;
        public Card(int number, int suit)
        {
            this.number = number;
            this.suit = suit;
        }
    }
    public class GameLogic // Klassen der styrer spillet
    {
        List<Card> Deck = new List<Card>(); // En liste af kort
        public GameLogic()
        {
            for (int i = 1; i < 5; i++) // 1 = hearts, 2 = diamonds, 3 = clubs, 4 = spades
            {
                for (int j = 1; j < 14; j++) // 1 = ace, 2-10 = 2-10, 11 = jack, 12 = queen, 13 = king
                {
                    Deck.Add(new Card(j, i)); // Tilføjer et kort til bunken
                }
            }
        }

        public void ShuffleDeck() // Blander bunken
        {
            Random rng = new Random();
            int n = Deck.Count; // Antal kort i bunken
            while (n > 1) // Fisher-Yates shuffle
            {
                n--; // n = n - 1
                int k = rng.Next(n + 1); // Random number mellem 0 og n
                Card value = Deck[k]; // Gemmer kortet på plads k
                Deck[k] = Deck[n]; // Bytter plads på kortene
                Deck[n] = value; // Bytter plads på kortene
            }
        }
    }
}