namespace poker_spil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameLogic game = new GameLogic(); // Opretter et nyt spil
            game.DealCards(); // Giver kort til spillerne

            for (int i = 0; i < game.players.Length; i++)
            {
                Console.WriteLine($"Player {i + 1}:");
                Console.WriteLine($"Card 1: {game.players[i].cards[0].number} of suit {game.players[i].cards[0].suit}");
                Console.WriteLine($"Card 2: {game.players[i].cards[1].number} of suit {game.players[i].cards[1].suit}");
                Console.WriteLine();
            }
        }
    }
    public class Player
    {
        public Card[] cards = new Card[2]; // Changed to public
    }
    public class Card
    {
        public int number;
        public int suit;
        public Card(int number, int suit)
        {
            this.number = number;
            this.suit = suit;
        }
    }

    public class GameLogic // Klassen der styrer spillet
    {
        public List<Card> Deck = new List<Card>(); // En liste af kort
        public Player[] players = new Player[4]; // En liste af spillere
        public GameLogic()
        {
            for (int i = 1; i < 5; i++) // 1 = hearts, 2 = diamonds, 3 = clubs, 4 = spades
            {
                for (int j = 1; j < 14; j++) // 1 = ace, 2-10 = 2-10, 11 = jack, 12 = queen, 13 = king
                {
                    Deck.Add(new Card(j, i)); // Tilføjer et kort til bunken
                }
            }

            for (int i = 0; i < 4; i++) // Opretter 4 spillere
            {
                players[i] = new Player();
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

        public void DealCards() // Giver kort til spillerne
        {
            ShuffleDeck(); // Blander bunken

            for (int i = 0; i < players.Length; i++) // For hver spiller
            {
                players[i].cards[0] = Deck[0]; // Giver spilleren et kort
                Deck.RemoveAt(0); // Fjerner kortet fra bunken
                players[i].cards[1] = Deck[0]; // Giver spilleren et kort
                Deck.RemoveAt(0); // Fjerner kortet fra bunken
            }
        }
    }
}