using System;
using System.Collections.Generic;

namespace Snap.Models
{
    public class Game
    {
        public Game(int? level = null)
        {
            Level = level ?? 1;
            Deck = new List<Card>();
            PlayerStack = new List<Card>();
            ComputerStack = new List<Card>();
            SharedStack = new List<Card>();
            IsPlayerTurn = true;
        }

        public int Level { get; set; }
        public List<Card> Deck { get; set; }
        public List<Card> PlayerStack { get; set; }
        public List<Card> ComputerStack { get; set; }
        public List<Card> SharedStack { get; set; }
        public bool IsPlayerTurn { get; set; }

        public bool InPlay
        {
            get
            {
                return Deck.Count == 0 && !(PlayerStack.Count == 0 && IsPlayerTurn) && !(ComputerStack.Count == 0 && !IsPlayerTurn);
            }
        }

        public int TurnDuration
        {
            get
            {
                int baseDuration = 1000;
                double decreaseRate = 0.9;
                double turnDuration = (baseDuration / decreaseRate) * Math.Pow(decreaseRate, Level);
                return (int)Math.Round(turnDuration);
            }
        }

        public bool HasSnap
        {
            get
            {
                return SharedStack.Count > 1 && SharedStack[0].Rank == SharedStack[1].Rank;
            }
        }

        public Status Status
        {
            get
            {
                if (PlayerStack.Count > ComputerStack.Count)
                {
                    return Status.Win;
                }
                else if (ComputerStack.Count > PlayerStack.Count)
                {
                    return Status.Lose;
                }
                else
                {
                    return Status.Draw;
                }
            }
        }
    }
}
