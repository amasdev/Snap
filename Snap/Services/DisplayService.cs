using Snap.Models;
using System;
using System.Threading;

namespace Snap.Services
{
    public class DisplayService : IDisplayService
    {
        public void Welcome()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Snap!\n");
            Console.WriteLine("Snap is a simple card game in which the player (\"YOU\")\n" +
                "has to react before the computer (\"COM\") to call a SNAP\n" +
                "by pressing any key when the rank of the current card\n" +
                "matches the rank of the previous card.\n");
            Console.WriteLine("If YOU call SNAP faster than COM then YOU will collect\n" +
                "the cards played. If COM calls faster or YOU call SNAP\n" +
                "incorrectly, COM collects the cards.\n");
            Console.WriteLine("The level is over when YOU or COM have no cards left to\n" +
                "play. If you win you can continue to the next level\n" +
                "where you'll have to react even faster to call a SNAP!\n");
            Console.WriteLine("Press any key to play");
        }

        public void GetReady(int turnDuration)
        {
            Console.Clear();
            Console.WriteLine("Get ready to SNAP... 3");
            Thread.Sleep(turnDuration);

            Console.Clear();
            Console.WriteLine("Get ready to SNAP... 2");
            Thread.Sleep(turnDuration);

            Console.Clear();
            Console.WriteLine("Get ready to SNAP... 1");
            Thread.Sleep(turnDuration);
        }

        public void TopCard(Card card, bool isPlayerTurn)
        {
            Console.Clear();
            Console.WriteLine($"{(isPlayerTurn ? "YOU" : "COM")}: {card.Rank} of {card.Suit}");
        }

        public void SnapCalled(bool isPlayer)
        {
            Console.Clear();
            Console.WriteLine($"{(isPlayer ? "YOU" : "COM")}: SNAP!\n");
            Thread.Sleep(2000);
        }

        public void SnapCorrect(bool isPlayer, int collectCount)
        {
            Console.Clear();
            Console.WriteLine($"{(isPlayer ? "YOU were" : "COM was")} the fastest and " +
                $"collect{(isPlayer ? null : "s")} {collectCount} card{(collectCount == 1 ? null : "s")}\n");
        }

        public void SnapIncorrect(int collectCount)
        {
            Console.Clear();
            Console.WriteLine($"No SNAP - COM collects {collectCount} card{(collectCount == 1 ? null : "s")}\n");
        }

        public void StatusUpdate(Status status, bool inPlay, int playerStackCount, int computerStackCount)
        {
            int margin = Math.Abs(playerStackCount - computerStackCount);
            switch (status)
            {
                case Status.Win:
                    Console.WriteLine($"YOU {(inPlay ? "are leading" : "won")} by {margin} " +
                        $"card{(margin == 1 ? null : "s")}!");
                    break;
                case Status.Lose:
                    Console.WriteLine($"COM {(inPlay ? "is leading" : "won")} by {margin} " +
                        $"card{(margin == 1 ? null : "s")}");
                    break;
                case Status.Draw:
                    Console.WriteLine($"YOU and COM {(inPlay ? "are" : "finished")} level " +
                        $"with {playerStackCount} card{(playerStackCount == 1 ? null : "s")} each");
                    break;
            }
            Thread.Sleep(4000);
        }

        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("All cards played\n");
        }

        public void Continue(int currentLevel, int nextLevel)
        {
            Console.Clear();
            Console.WriteLine($"Press any key to {(nextLevel == currentLevel ? "re" : null)}play Level {nextLevel}");
        }
    }
}
