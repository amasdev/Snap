using Snap.Models;
using System.Collections.Generic;
using Xunit;

namespace Snap.Tests
{
    public class GameTests
    {
        [Fact]
        public void InPlay_False_IfDeckNotEmpty()
        {
            var game = new Game()
            {
                Deck = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Diamonds" }
                },
                PlayerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" },
                },
                ComputerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Hearts" }
                }
            };

            Assert.False(game.InPlay);
        }

        [Fact]
        public void InPlay_False_IfPlayerTurnAndPlayerStackEmpty()
        {
            var game = new Game()
            {
                IsPlayerTurn = true,
                PlayerStack = new List<Card>(),
                ComputerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Diamonds" }
                },
            };

            Assert.False(game.InPlay);
        }

        [Fact]
        public void InPlay_False_IfComputerTurnAndComputerStackEmpty()
        {
            var game = new Game()
            {
                IsPlayerTurn = false,
                PlayerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Diamonds" }
                },
                ComputerStack = new List<Card>()
            };

            Assert.False(game.InPlay);
        }

        [Fact]
        public void HasSnap_True_IfCardRanksAreEqual()
        {
            var game = new Game()
            {
                SharedStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" },
                    new Card { Rank = "A", Suit = "Diamonds" }
                }
            };

            Assert.True(game.HasSnap);
        }

        [Fact]
        public void HasSnap_False_IfCardRanksAreNotEqual()
        {
            var game = new Game()
            {
                SharedStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" },
                    new Card { Rank = "2", Suit = "Diamonds" }
                }
            };

            Assert.False(game.HasSnap);
        }

        [Fact]
        public void HasSnap_False_IfSharedStackCountLessThan2()
        {
            var game = new Game()
            {
                SharedStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" },
                }
            };

            Assert.False(game.HasSnap);
        }

        [Fact]
        public void TurnDuration_Maximum1000Ms_Always()
        {
            var game = new Game();
            int minDuration = 1;
            int maxDuration = 1000;

            Assert.InRange(game.TurnDuration, minDuration, maxDuration);
        }

        [Fact]
        public void Status_Win_IfPlayerStackCountGreater()
        {
            var game = new Game()
            {
                PlayerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" },
                    new Card { Rank = "A", Suit = "Diamonds" }
                },
                ComputerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Hearts" }
                }
            };

            Assert.Equal(Status.Win, game.Status);
        }

        [Fact]
        public void Status_Lose_IfComputerStackCountGreater()
        {
            var game = new Game()
            {
                PlayerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" }
                },
                ComputerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Hearts" },
                    new Card { Rank = "A", Suit = "Diamonds" }
                }
            };

            Assert.Equal(Status.Lose, game.Status);
        }

        [Fact]
        public void Status_Draw_IfEqualCountInPlayerStackAndComputerStack()
        {
            var game = new Game()
            {
                PlayerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Clubs" }
                },
                ComputerStack = new List<Card>
                {
                    new Card { Rank = "A", Suit = "Hearts" }
                }
            };

            Assert.Equal(Status.Draw, game.Status);
        }
    }
}
