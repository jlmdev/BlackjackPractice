using System;
using System.Collections.Generic;
using System.Linq;

namespace blackjack
{
    class Card
    {
        public string Suit { get; set; }
        public string Face { get; set; }
        public int Value()
        {
            switch (Face)
            {
                case "Ace":
                    return 11;
                case "2":
                    return 2;
                case "3":
                    return 3;
                case "4":
                    return 4;
                case "5":
                    return 5;
                case "6":
                    return 6;
                case "7":
                    return 7;
                case "8":
                    return 8;
                case "9":
                    return 9;
                case "10":
                    return 10;
                case "Jack":
                    return 10;
                case "Queen":
                    return 10;
                case "King":
                    return 10;
                default:
                    return 0;
            }
        }
    }

    class Player
    {
        public string PlayerName { get; set; }
        public List<Card> Hand = new List<Card>();
        public int HandValue()
        {
            int handTotal = 0;
            foreach (Card cardInHand in Hand)
            {
                handTotal = handTotal + cardInHand.Value();
            }
            return handTotal;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Prompt Player for Name
            Console.WriteLine("Welcome. What's your name? ");
            string userName = Console.ReadLine();


            //Play again loop
            var playAgain = "y";
            while (playAgain == "y")
            {

                // Create Deck List
                var deck = new List<Card>();
                // Define arrays of suits and faces for assignment
                var assignedSuit = new string[] { "Clubs", "Diamonds", "Hearts", "Spades" };
                var assignedFace = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

                // loops to assign card suits and faces
                for (var suitIndex = 0; suitIndex < assignedSuit.Length; suitIndex++)
                {
                    for (var faceIndex = 0; faceIndex < assignedFace.Length; faceIndex++)
                    {
                        // Assign suit and face to cards
                        string cardSuit = assignedSuit[suitIndex];
                        string cardFace = assignedFace[faceIndex];
                        var newCard = new Card();

                        {
                            newCard.Suit = cardSuit;
                            newCard.Face = cardFace;
                        }

                        deck.Add(newCard);
                    }
                }

                // Shuffling Algorithm
                // Consider changing this to method later
                var numberOfCards = deck.Count();
                var randomNumberGenerator = new Random();

                for (var rightIndex = numberOfCards - 1; rightIndex > 0; rightIndex--)
                {
                    var leftIndex = randomNumberGenerator.Next(rightIndex - 1);
                    var leftCard = deck[rightIndex];
                    var rightCard = deck[leftIndex];
                    deck[rightIndex] = rightCard;
                    deck[leftIndex] = leftCard;
                }

                // Create Dealer instance
                var dealer = new Player();
                {
                    dealer.PlayerName = "Dealer";

                }

                // Create Player instance
                var humanPlayer = new Player();
                {
                    humanPlayer.PlayerName = userName;
                }


                // Deal initial cards to Dealer
                dealer.Hand.Add(deck[0]);
                dealer.Hand.Add(deck[1]);

                // Remove dealt cards from Deck
                deck.RemoveAt(0);
                deck.RemoveAt(0);


                // Deal initial cards to the Player
                humanPlayer.Hand.Add(deck[0]);
                humanPlayer.Hand.Add(deck[1]);

                // Remove dealt cards from deck
                deck.RemoveAt(0);
                deck.RemoveAt(0);

                // Hit-Stand process
                var dealAgain = "y";
                var hitStandLoop = "y";
                while (dealAgain == "y")
                {

                    while (hitStandLoop != "n")
                    {
                        // Show Player cards
                        Console.WriteLine($"{humanPlayer.PlayerName}'s cards:");
                        foreach (var playerCard in humanPlayer.Hand)
                        {
                            Console.WriteLine($"{playerCard.Face} of {playerCard.Suit} with value of {playerCard.Value()}");
                        }

                        // Give the current value of the hand
                        Console.WriteLine($"Your current hand is worth {humanPlayer.HandValue()} points");

                        // Check for Bust condition
                        if (humanPlayer.HandValue() > 21)
                        {
                            dealAgain = "n";
                            hitStandLoop = "n";
                            break;
                        }

                        // Give the current value of the hand

                        Console.WriteLine("Do you want to hit or stand?");
                        var hitStandResponse = Console.ReadLine();
                        switch (hitStandResponse)
                        {
                            case "h":
                                humanPlayer.Hand.Add(deck[0]);
                                deck.RemoveAt(0);
                                hitStandLoop = "y";
                                break;
                            case "s":
                                dealAgain = "n";
                                hitStandLoop = "n";
                                break;
                            default:
                                Console.WriteLine("H or S, please.");
                                break;
                        }

                    }
                }
                // Play Dealer Hand
                // Reveal Dealer's Hand

                var dealerStand = "default";
                while (dealerStand != "y")
                {
                    Console.WriteLine("Dealer's Hand:");
                    foreach (var dealerCard in dealer.Hand)
                    {
                        Console.WriteLine($"{dealerCard.Face} of {dealerCard.Suit} with value of {dealerCard.Value()}");
                    }

                    Console.WriteLine($"Dealer's hand is worth {dealer.HandValue()} points");

                    // Check value of Dealer's hand
                    if (humanPlayer.HandValue() > 21)
                    {
                        dealerStand = "y";
                    }
                    else if ((dealer.HandValue() < 17) && (humanPlayer.HandValue() > 21))
                    {
                        dealerStand = "y";
                    }
                    else if ((dealer.HandValue() < 17) && (humanPlayer.HandValue() < 22))
                    {
                        dealer.Hand.Add(deck[0]);
                        deck.RemoveAt(0);
                        dealerStand = "n";
                    }
                    else if (dealer.HandValue() > 21)
                    {
                        dealerStand = "y";
                    }
                    else
                    {
                        dealerStand = "y";
                    }
                }

                // Calculate Winner
                if (humanPlayer.HandValue() > 21)
                {
                    Console.WriteLine($"{humanPlayer.PlayerName} busts");

                }
                else if (dealer.HandValue() > 21)
                {
                    Console.WriteLine("Dealer busts");
                }
                else if (humanPlayer.HandValue() > dealer.HandValue())
                {
                    Console.WriteLine($"{humanPlayer.PlayerName} wins!");
                }
                else
                {
                    Console.WriteLine("Dealer wins.");
                }

                // Prompt for play again
                Console.WriteLine("Would you like to play again? (y/n)");
                playAgain = Console.ReadLine();
            }
        }

    }
}