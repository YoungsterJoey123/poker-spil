using System.Security.Cryptography;

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
        public int handvalue;
        public Player(int handvalue) 
        {
            this.handvalue = handvalue;
        }
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
                players[i] = new Player(0);
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

            //for (int i = 0; i < 5; i++)
            //{
            //    tableCards[i] = Deck[0]; // Giver bordet et kort
            //    Deck.RemoveAt(0); // Fjerner kortet fra bunken
            //}
            tableCards[0] = new Card(1, 1);
            tableCards[1] = new Card(1, 2);
            tableCards[2] = new Card(1, 4);
            tableCards[3] = new Card(9, 1);
            tableCards[4] = new Card(8, 1);
        }

        public void CheckHand()
        {

            for(int i = 0; i < players.Length; i++)
            {
                Card[] combined = new Card[tableCards.Length + 2];
                tableCards.CopyTo(combined, 0);
                players[i].cards.CopyTo(combined, tableCards.Length);

                Card[] highCard = new Card[3];
                List<Card> combinedList = combined.ToList();

                int HeartCounter = 0;
                int DiamondCounter = 0;
                int ClubCounter = 0;
                int SpadeCounter = 0;

                int pairCounter = 0;
                for (int j = 0; j < combined.Length; j++)
                {
                    for (int k = j + 1; k < combined.Length; k++)
                    {
                        if (combined[j].number == combined[k].number)
                        {
                            pairCounter++;
                        }
                    }
                }
                if (pairCounter >= 1) // par
                {
                    Console.WriteLine($"Player {i + 1} has a pair");
                    players[i].handvalue = 2;
                    pairCounter = 0;
                }

                int twoPair = 0;
                int twoPairCounter = 0;
                for (int j = 0; j < combined.Length; j++)
                {
                    for (int k = j + 1; k < combined.Length; k++)
                    {
                        if (combined[j].number == combined[k].number)
                        {
                            twoPairCounter++;
                        }
                    }
                    if (twoPairCounter >= 2) // two pair
                    {
                        Console.WriteLine($"Player {i + 1} has two pair");
                        players[i].handvalue = 4;
                        twoPairCounter = 0;
                        twoPair = 1;
                        break;
                    }
                }

                int threeOfAKind = 0;
                int threeOfAKindCounter = 0;
                for (int j = 0; j < combined.Length; j++)
                {
                    for (int k = j + 1; k < combined.Length; k++)
                    {
                        if (combined[j].number == combined[k].number)
                        {
                            threeOfAKindCounter++;
                        }
                    }
                    if (threeOfAKindCounter >= 3) // three of a kind
                    {
                        Console.WriteLine($"Player {i + 1} has three of a kind");
                        players[i].handvalue = 3;
                        threeOfAKindCounter = 0;
                        threeOfAKind = 1;
                        break;
                    }
                }

                for (int j = 0; j < highCard.Length; j++)
                {
                    int highestcardindex = 0;
                    for (int k = 0; k < combinedList.Count; k++)
                    {
                        if (combinedList[highestcardindex].number < combinedList[k].number)
                        {
                            highestcardindex = k;
                        }
                    }
                    highCard[j] = combinedList[highestcardindex];
                    combinedList.RemoveAt(highestcardindex);
                }
                int StraightCounter = 0;
                int num = 1;
                for (int h = 0; h < highCard.Length; h++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < combined.Length; k++)
                        {
                            if (combined[k].number == highCard[h].number - num)
                            {
                                num++;
                                StraightCounter++;
                                highCard[h] = combined[k];
                            }
                        }
                    }
                }
                if (StraightCounter == 5) //straight
                {
                    Console.WriteLine($"Player {i + 1} has a straight");
                    players[i].handvalue = 4;
                    StraightCounter = 0;
                }

                for (int j = 0; j < combined.Length; j++)
                {
                    if (combined[j].suit == 1)
                    {
                        HeartCounter++;
                    }
                    if (combined[j].suit == 2)
                    {
                        DiamondCounter++;
                    }
                    if (combined[j].suit == 3)
                    {
                        ClubCounter++;
                    }
                    if (combined[j].suit == 4)
                    {
                        SpadeCounter++;
                    }
                }
                if (HeartCounter >= 5 || DiamondCounter >= 5 || ClubCounter >= 5 || SpadeCounter >= 5) // flush
                {
                    Console.WriteLine($"Player {i + 1} has a flush");
                    players[i].handvalue = 5;
                    HeartCounter = 0;
                    DiamondCounter = 0;
                    ClubCounter = 0;
                    SpadeCounter = 0;
                }


                if (threeOfAKind  == 1 && twoPair == 1) // fullhouse
                {
                    Console.WriteLine($"Player {i + 1} has a fullhouse");
                    players[i].handvalue = 5;
                    threeOfAKind = 0;
                    twoPair = 0;
                }


                int fourOfAKindCounter = 0;
                for (int j = 0; j < combined.Length; j++)
                {
                    for (int k = j + 1; k < combined.Length; k++)
                    {
                        if (combined[j].number == combined[k].number)
                        {
                            fourOfAKindCounter++;
                        }
                    }
                    if (fourOfAKindCounter >= 4) // four of a kind
                    {
                        Console.WriteLine($"Player {i + 1} has four of a kind");
                        players[i].handvalue = 6;
                        fourOfAKindCounter = 0;
                        break;
                    }
                }













                for (int j = 0; j < combined.Length;j++)
                {
                    if (combined[j].number == 12)
                    {
                        for(int k = 0; k < combined.Length; k++)
                        {
                            if (combined[k].number == 11 && combined[k].suit == combined[j].suit)
                            {
                                for (int l = 0; l < combined.Length; l++)
                                {
                                    if (combined[l].number == 10 && combined[l].suit == combined[j].suit)
                                    {
                                        for (int h = 0; h < combined.Length; h++)
                                        {
                                            if (combined[h].number == 9 && combined[h].suit == combined[j].suit)
                                            {
                                                for (int g = 0; g < combined.Length; g++)
                                                {
                                                    if (combined[g].number == 8 && combined[g].suit == combined[j].suit)
                                                    {
                                                        Console.WriteLine($"Player {i + 1} has a Royal flush");
                                                        players[i].handvalue = 10000000;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }                    
            }
            int bestValue = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].handvalue > bestValue)
                {
                    bestValue = players[i].handvalue;
                }
            }
            for (int i= 0; i < players.Length; i++)
            {
                if (players[i].handvalue == bestValue)
                {
                    Console.WriteLine($"Player {i + 1} has won");
                }
            }
        }
    }
}