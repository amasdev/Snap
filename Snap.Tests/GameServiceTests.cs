using Snap.Services;
using System;
using System.Linq;
using Xunit;

namespace Snap.Tests
{
    public class GameServiceTests
    {
        private readonly IGameService gameService = new GameService();

        [Fact]
        public void CreateGame_GameNotNull_Always()
        {
            gameService.CreateGame();

            Assert.NotNull(gameService.Game);
        }

        [Fact]
        public void CreateDeck_DeckHas52DistinctCards_Always()
        {
            gameService.CreateGame();
            gameService.CreateDeck();

            Assert.Equal(52, gameService.Game.Deck.Distinct().Count());
        }

        [Fact]
        public void ShuffleDeck_DeckSequenceChanged_Always()
        {
            gameService.CreateGame();
            gameService.CreateDeck();

            var deckCopy = gameService.Game.Deck.ToList();
            gameService.ShuffleDeck();

            Assert.False(gameService.Game.Deck.SequenceEqual(deckCopy), "Deck sequence hasn't changed");
        }

        [Fact]
        public void DealCards_DeckEmpty_Always()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();

            Assert.Empty(gameService.Game.Deck);
        }

        [Fact]
        public void DealCards_PlayerStackHas26Cards_Always()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();

            Assert.Equal(26, gameService.Game.PlayerStack.Count);
        }

        [Fact]
        public void DealCards_ComputerStackHas26Cards_Always()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();

            Assert.Equal(26, gameService.Game.ComputerStack.Count);
        }

        [Fact]
        public void PlayCard_PlayerStackDecreasesBy1_IfPlayerPlaysCard()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();

            int prePlayCount = gameService.Game.PlayerStack.Count;
            gameService.PlayCard();
            int postPlayCount = gameService.Game.PlayerStack.Count;

            Assert.Equal(1, prePlayCount - postPlayCount);
        }

        [Fact]
        public void PlayCard_ComputerStackDecreasesBy1_IfComputerPlaysCard()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();
            gameService.PlayCard();
            gameService.TogglePlayerTurn();

            int prePlayCount = gameService.Game.ComputerStack.Count;
            gameService.PlayCard();
            int postPlayCount = gameService.Game.ComputerStack.Count;

            Assert.Equal(1, prePlayCount - postPlayCount);
        }

        [Fact]
        public void CollectCards_SharedStackIsEmpty_Always()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();
            gameService.PlayCard();
            gameService.TogglePlayerTurn();
            gameService.PlayCard();
            gameService.CollectCards(forPlayer: true);

            Assert.Empty(gameService.Game.SharedStack);
        }

        [Fact]
        public void CollectCards_PlayerStackIncreasesBySharedStackCount_IfPlayerCollects()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();
            gameService.PlayCard();
            gameService.TogglePlayerTurn();
            gameService.PlayCard();

            int preCollectPlayerCount = gameService.Game.PlayerStack.Count;
            int preCollectSharedCount = gameService.Game.SharedStack.Count;
            gameService.CollectCards(forPlayer: true);
            int postCollectPlayerCount = gameService.Game.PlayerStack.Count;

            Assert.Equal(postCollectPlayerCount, preCollectPlayerCount + preCollectSharedCount);
        }

        [Fact]
        public void CollectCards_ComputerStackIncreasesBySharedStackCount_IfComputerCollects()
        {
            gameService.CreateGame();
            gameService.CreateDeck();
            gameService.DealCards();
            gameService.PlayCard();
            gameService.TogglePlayerTurn();
            gameService.PlayCard();

            int preCollectComputerCount = gameService.Game.ComputerStack.Count;
            int preCollectSharedCount = gameService.Game.SharedStack.Count;
            gameService.CollectCards(forPlayer: false);
            int postCollectComputerCount = gameService.Game.ComputerStack.Count;

            Assert.Equal(postCollectComputerCount, preCollectComputerCount + preCollectSharedCount);
        }
    }
}
