using Snap.Models;
using Snap.Services;
using System;
using System.Linq;
using System.Threading;

namespace Snap
{
    public class AppService : IAppService
    {
        private readonly IGameService gameService;
        private readonly IDisplayService displayService;

        public AppService(IGameService gameService, IDisplayService displayService)
        {
            this.gameService = gameService;
            this.displayService = displayService;
        }

        public void Start()
        {
            displayService.Welcome();
            Console.ReadKey();
            Play(1);
        }

        public void Play(int level)
        {
            gameService.CreateGame(level);
            gameService.CreateDeck();
            gameService.ShuffleDeck();
            gameService.DealCards();

            var game = gameService.Game;
            displayService.GetReady(game.TurnDuration);

            while (game.InPlay)
            {
                gameService.PlayCard();
                displayService.TopCard(game.SharedStack.First(), game.IsPlayerTurn);
                gameService.TogglePlayerTurn();
                Thread.Sleep(game.TurnDuration);

                bool keyPressed = Console.KeyAvailable && Console.ReadKey() != null;
                bool snapCalled = false;

                if (game.HasSnap)
                {
                    snapCalled = true;
                    displayService.SnapCalled(isPlayer: keyPressed);
                    displayService.SnapCorrect(isPlayer: keyPressed, game.SharedStack.Count);
                    gameService.CollectCards(forPlayer: keyPressed);
                }
                else if (keyPressed)
                {
                    snapCalled = true;
                    displayService.SnapCalled(isPlayer: true);
                    displayService.SnapIncorrect(game.SharedStack.Count);
                    gameService.CollectCards(forPlayer: false);
                }

                if (snapCalled)
                {
                    displayService.StatusUpdate(game.Status, game.InPlay, game.PlayerStack.Count, game.ComputerStack.Count);
                    displayService.GetReady(game.TurnDuration);
                }
            }

            displayService.GameOver();
            displayService.StatusUpdate(game.Status, game.InPlay, game.PlayerStack.Count, game.ComputerStack.Count);

            int nextLevel = game.Status == Status.Win ? game.Level + 1 : game.Level;
            displayService.Continue(game.Level, nextLevel);
            Console.ReadKey();
            Play(nextLevel);
        }
    }
}
