using Snap.Models;

namespace Snap.Services
{
    public interface IGameService
    {
        Game Game { get; }

        void CollectCards(bool forPlayer);
        void CreateDeck();
        void CreateGame(int? level = null);
        void DealCards();
        void PlayCard();
        void ShuffleDeck();
        void TogglePlayerTurn();
    }
}