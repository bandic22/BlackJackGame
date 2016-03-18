using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Program
    {
        static void Main(string[] args)
        {            
            // instantiate player and dealer
            var player = new Player();
            var dealer = new Dealer();
            // intitializes scores to zero
            player.Score = 0;
            dealer.Score = 0;
            // instantiate hand classes
            var playerHand = new List<Card>();
            var dealerHand = new List<Card>();
            // instantiate deck, rnd and gameloop
            var deck = new Deck();            
            var rnd = new Random();
            var gameLoop = new GameLoop(player, dealer, playerHand, dealerHand);
            // welcome message and get player name
            gameLoop.Intro(player);
            // continues to loop game while AskNewGame is true, is initialised as true
            while (gameLoop.AskNewGame(player, playerHand, dealer, dealerHand) == true)
            {//plays main gameloop and then creates a new randomized deck when the gameloop ends, deals new hands to player and dealer, resets scores
                gameLoop.PlayerDeal(player, deck, rnd, playerHand);
                gameLoop.DealerDeal(dealer, deck, rnd, dealerHand);
                gameLoop.PlayGame(player, dealer, deck, rnd, playerHand, dealerHand);
                deck = new Deck();
                player.Score = 0;
                dealer.Score = 0;
                playerHand = new List<Card>();
                dealerHand = new List<Card>();
            } 
            gameLoop.AskNewGame(player, playerHand, dealer, dealerHand);
        }
    }

    public class Card
    {
        public string Name { get; set; }
        public string Suit { get; set; }
        public int Value { get; set; }
    }

    public class Deck
    {
        public List<Card> Cards { get; set; }
        public string[] FaceValues { get; set; }
        public string[] Suits { get; set; }
        public int[] ActualValues { get; set; }

        public Deck()
        {
            // instantiate cards as a new list of Card class
            this.Cards = new List<Card>();

            FaceValues = new string[]
            {
                "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"
            };

            Suits = new string[]
            {
                "Hearts", "Clubs", "Spades", "Diamonds"
            };

            ActualValues = new int[]
            {
                11, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10
            };

            for (var j = 0; j < 4; j++)
            {
                for (var i = 0; i < 13; i++)
                {
                    Cards.Add(new Card() { Name = FaceValues[i], Suit = Suits[j], Value = ActualValues[i] });
                }
            }
        }
    }

    public class GameLoop
    {
      
        public GameLoop(Player player, Dealer dealer, List<Card> playerHand, List<Card> dealerHand)
        { 
            
        }

        public void Intro(Player player)
        {
            Console.WriteLine("Enter your name:");
            player.Name = Console.ReadLine();
            Console.WriteLine(String.Format("\nWelcome to Blackjack Simulator 3000, {0}!\n", player.Name));
        }

        /// <summary>
        /// Gets a new random number, deals random cards from the deck, increases scores based on given cards, gives the players property, 'Hand' the value of the instantiated hand class, playerHand/dealerHand
        /// </summary>
        /// <param name="player"></param>
        /// <param name="deck"></param>
        /// <param name="rnd"></param>
        /// <param name="playerHand"></param>
        public void PlayerDeal(Player player, Deck deck, Random rnd, List<Card> playerHand)
        {
            try {
                var randNum1 = rnd.Next(deck.Cards.Count);
                var randNum2 = rnd.Next(deck.Cards.Count);
                var rndCard1 = deck.Cards[randNum1];
                playerHand.Add(rndCard1);
                player.Score += rndCard1.Value;
                deck.Cards.Remove(rndCard1);
                var rndCard2 = deck.Cards[randNum2];
                playerHand.Add(rndCard2);
                player.Score += rndCard2.Value;
                deck.Cards.Remove(rndCard2);
                player.Hand = playerHand;
            }
            catch
            {
                Console.WriteLine("\nError! Randomizer has had an issue. Please restart the application");
                Console.ReadLine();
            }
        }
        /// <summary>
        /// Gets a new random number, deals random cards from the deck, increases scores based on given cards, gives the players property, 'Hand' the value of the instantiated hand class, playerHand/dealerHand
        /// </summary>
        /// <param name="player"></param>
        /// <param name="deck"></param>
        /// <param name="rnd"></param>
        /// <param name="playerHand"></param>
        public void DealerDeal(Dealer dealer, Deck deck, Random rnd, List<Card> dealerHand)
        {
            try
            {
                var randNum3 = rnd.Next(deck.Cards.Count);
                var randNum4 = rnd.Next(deck.Cards.Count);
                var rndCard3 = deck.Cards[randNum3];
                dealerHand.Add(rndCard3);
                dealer.Score += rndCard3.Value;
                deck.Cards.Remove(rndCard3);
                var rndCard4 = deck.Cards[randNum4];
                dealer.Score += rndCard4.Value;
                dealerHand.Add(rndCard4);
                deck.Cards.Remove(rndCard4);
                dealer.Hand = dealerHand;
            }
            catch 
            {
                Console.WriteLine("Error! Randomizer has had an issue. Please restart the application.");
                Console.ReadLine();
            }
            
        }
        /// <summary>
        /// Calls the method to notify player of win or lose result
        /// </summary>
        public void PlayerWin()
        {
            Console.WriteLine("You Win!");
            Console.ReadLine();
        }

        public void PlayerLose()
        {
            Console.WriteLine("You Lose!");
            Console.ReadLine();
        }
        /// <summary>
        /// Initializes player turn to true. Gives user a breakdown of the hands and scores, and asks for first reaction. Contains the rest of the logic for every possible outcome.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="dealer"></param>
        /// <param name="deck"></param>
        /// <param name="rnd"></param>
        /// <param name="playerHand"></param>
        /// <param name="dealerHand"></param>
        public void PlayGame(Player player, Dealer dealer, Deck deck, Random rnd, List<Card> playerHand, List<Card> dealerHand)
        {
            var playerTurn = true;

            Console.WriteLine(String.Format("Your hand:\n\n{0} of {1}\n{2} of {3}\n\nThe Dealer's hand:\n\n{4} of {5}\nHidden", player.Hand[0].Name, player.Hand[0].Suit, player.Hand[1].Name, player.Hand[1].Suit, dealer.Hand[0].Name, dealer.Hand[0].Suit, dealer.Hand[1].Name, dealer.Hand[1].Suit));
            Console.WriteLine(String.Format("\nNumber of cards in deck: {0}\n", deck.Cards.Count));
            Console.WriteLine(String.Format("\nYour Score: {0}\nDealer Score: Hidden\n", player.Score, dealer.Score));
            Console.WriteLine("Would you like to:\n\n1: Hit\n2: Stay\n");
            player.PlayerInput = Console.ReadLine();

            while (player.Score <= 21 && dealer.Score <= 21)
            {
                while (player.Score <= 21 && playerTurn)
                {
                    // takes in user input, and if user picks hit, new random card is removed from deck and placed into player hand, then it asks for next input. Continues while it's the player's turn.
                    if (player.PlayerInput == "1")
                    {
                        var rndNum = rnd.Next(deck.Cards.Count);
                        var rndCard = deck.Cards[rndNum];
                        playerHand.Add(rndCard);
                        player.Score += rndCard.Value;
                        deck.Cards.Remove(rndCard);
                        Console.WriteLine(String.Format("\nYou have been dealt a {0} of {1}\nYour score is: {2}", rndCard.Name, rndCard.Suit, player.Score));
                        Console.WriteLine(String.Format("\nNumber of cards in deck: {0}", deck.Cards.Count));
                        Console.WriteLine("Would you like to:\n\n1: Hit\n2: Stay\n");
                        player.PlayerInput = Console.ReadLine();

                    }
                    else if (player.PlayerInput == "2")
                    {
                        Console.WriteLine("\nYou stay");
                        Console.ReadLine();
                        playerTurn = false;
                    }
                }
                if(player.Score > 21)
                {
                    for (int i = 0; i < playerHand.Count; i++)
                    {
                        if (playerHand[i].Name == "Ace")
                        {
                            playerHand[i].Value = 1;
                        }
                        else
                        {
                            PlayerLose();
                        }
                    }
                }
                else
                {
                    playerTurn = false;
                }
                // while it's the dealer's turn, contains logic for all different outcomes of the game
                while (!playerTurn && dealer.Score <= 17 || player.Score > dealer.Score && (dealer.Score >= 17))
                {
                    var rndNum = rnd.Next(deck.Cards.Count);
                    var rndCard = deck.Cards[rndNum];

                    dealerHand.Add(rndCard);
                    deck.Cards.Remove(rndCard);
                    dealer.Score += rndCard.Value;
                    dealer.Hand = dealerHand;
                    Console.WriteLine(String.Format("\nThe dealer has been dealt a {0} of {1}\nHis score is: {2}", rndCard.Name, rndCard.Suit, dealer.Score));
                    Console.ReadLine();

                    if (dealer.Score > 21)
                    {
                        for (int i = 0; i < dealerHand.Count; i++)
                        {
                            if (dealerHand[i].Name == "Ace")
                            {
                                dealerHand[i].Value = 1;
                            }
                            else
                            {
                                PlayerWin();
                            }
                        }
                    }
                    else if (dealer.Score > 21 && player.Score <= 21)
                    {
                        PlayerWin();
                    }
                    else if (dealer.Score <= 21 && player.Score > 21)
                    {
                        PlayerLose();
                    }
                    else if (dealer.Score >= 17 && player.Score >= 17 && (dealer.Score == player.Score))
                    {
                        PlayerLose();
                    }
                }
                if (player.Score <= 21 && (dealer.Score >= player.Score && dealer.Score <= 21))
                {
                    PlayerLose();
                }
                else if (player.Score <= 21 && (dealer.Score <= 21 && dealer.Score == player.Score))
                {
                    PlayerLose();
                }
                else if (player.Score > 21)
                {
                    PlayerLose();
                }
                else if (dealer.Score > 17 && dealer.Score <= 21 && (dealer.Score >= player.Score))
                {
                    PlayerLose();
                }
                else if (dealer.Score >= 17 && dealer.Score <= 21 && player.Score <= 21 && (dealer.Score > player.Score))
                {
                    PlayerLose();
                }
            }
        }

        /// <summary>
        /// Prompts user to start a new game, then reinitializes hands if true
        /// </summary>
        /// <param name="player"></param>
        /// <param name="playerHand"></param>
        /// <param name="dealer"></param>
        /// <param name="dealerHand"></param>
        /// <returns></returns>
        public bool AskNewGame(Player player, List<Card> playerHand, Dealer dealer, List<Card> dealerHand)
        {
            Console.WriteLine("\nWould you like to play a round?\n\n1: Yes\n2: No");
            player.PlayerInput = Console.ReadLine();
            if (player.PlayerInput == "1")
            {
                ReInitHands(player, playerHand);
                ReInitHands(dealer, dealerHand);
                return true;            
            }
            else if (player.PlayerInput == "2")
            {
                return false;
            }
            return false;
        }
        
        public void ReInitHands(Player player, List<Card> playerHand)
        {
            if (playerHand.Count != 0)
            {
                playerHand.RemoveRange(0, 1);
                player.Hand = playerHand;
            }            
        }

        public void ReInitHands(Dealer dealer, List<Card> dealerHand)
        {
            if (dealerHand.Count != 0)
            {
                dealerHand.RemoveRange(0, 1);
                dealer.Hand = dealerHand;
            }
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
        public string PlayerInput { get; set; }
        public int Score { get; set; }
    }

    public class Dealer
    {
        public List<Card> Hand { get; set; }
        public int Score { get; set; }
    }
}
