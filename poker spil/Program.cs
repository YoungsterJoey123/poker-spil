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

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Table card {i + 1}: {game.tableCards[i].number} of suit {game.tableCards[i].suit}");
            }
            Console.WriteLine();

            game.CheckHand();


        }
    }
    public class Player
    {
        public Card[] cards = new Card[2]; 
        public int HighCard;
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
        public Card[] tableCards = new Card[5]; // En liste af bordkort
        public List<Card> Deck = new List<Card>(); // En liste af kort
        public Player[] players = new Player[4]; // En liste af spillere
        public GameLogic()
        {
            for (int i = 1; i < 5; i++) // 1 = hearts, 2 = diamonds, 3 = clubs, 4 = spades
            {
                for (int j = 0; j < 13; j++) // 0 = 2, 1 = 3, 2 = 4, ..., 12 = ace
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

            for (int i = 0; i < 5; i++)
            {
                tableCards[i] = Deck[0]; // Giver bordet et kort
                Deck.RemoveAt(0); // Fjerner kortet fra bunken
            }
        }

        public void CheckHand()
        {
            for(int i = 0; i < players.Length; i++)
            {
                int handvalue = 0;
                if (tableCards[0].suit == tableCards[1].suit &&
                    tableCards[0].suit == tableCards[2].suit &&
                    tableCards[0].suit == players[i].cards[0].suit &&
                    tableCards[0].suit == players[i].cards[1].suit)
                {
                    Card[] combined = new Card[tableCards.Length + 2];
                    combined.CopyTo(tableCards, 0);
                    combined.CopyTo(players[i].cards, tableCards.Length - 1);
                    int royalflushcounter = 0;
                    for (int j = 0; j < combined.Length; j++)
                    {
                        if (combined[j].number == 8)
                        {
                            royalflushcounter++;
                        }
                        if (combined[j].number == 9)
                        {
                            royalflushcounter++;
                        }
                        if (combined[j].number == 10)
                        {
                            royalflushcounter++;
                        }
                        if (combined[j].number == 11)
                        {
                            royalflushcounter++;
                        }
                        if (combined[j].number == 12)
                        {
                            royalflushcounter++;
                        }
                    }
                    if (royalflushcounter == 5)
                    {
                        Console.WriteLine($"Player {i + 1} has a royal flush");
                        handvalue = 1000000000;
                    }
                }
            }
        }
    }
}