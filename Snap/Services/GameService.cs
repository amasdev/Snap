using Snap.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Snap.Services
{
    public class GameService : IGameService
    {
        public Game Game { get; private set; }

        public void CreateGame(int? level = null)
        {
            Game = new Game(level);
        }

        public void CreateDeck()
        {
            var cards = new List<Card>();
            string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
            string[] ranks = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    cards.Add(new Card { Rank = rank, Suit = suit });
                }
            }
            Game.Deck = cards;
        }

        public void ShuffleDeck()
        {
            var random = new Random();
            Game.Deck = Game.Deck
                .OrderBy(x => random.Next())
                .ToList();
        }

        public void DealCards()
        {
            bool playerCardNext = true;
            while (Game.Deck.Count > 0)
            {
                var toStack = playerCardNext
                    ? Game.PlayerStack
                    : Game.ComputerStack;
                int deckIndex = Game.Deck.Count - 1;
                toStack.Add(Game.Deck[deckIndex]);
                Game.Deck.RemoveAt(deckIndex);
                playerCardNext = !playerCardNext;
            }
        }

        public void PlayCard()
        {
            var fromStack = Game.IsPlayerTurn
                ? Game.PlayerStack
                : Game.ComputerStack;
            int fromIndex = fromStack.Count - 1;
            Game.SharedStack.Insert(0, fromStack[fromIndex]);
            fromStack.RemoveAt(fromIndex);
        }

        public void TogglePlayerTurn()
        {
            Game.IsPlayerTurn = !Game.IsPlayerTurn;
        }

        public void CollectCards(bool forPlayer)
        {
            var toStack = forPlayer
                ? Game.PlayerStack
                : Game.ComputerStack;
            toStack.Reverse();
            toStack.AddRange(Game.SharedStack);
            toStack.Reverse();
            Game.SharedStack = new List<Card>();
        }
    }
}
