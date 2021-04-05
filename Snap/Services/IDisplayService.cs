using Snap.Models;

namespace Snap.Services
{
    public interface IDisplayService
    {
        void Continue(int currentLevel, int nextLevel);
        void GameOver();
        void GetReady(int turnDuration);
        void SnapCalled(bool isPlayer);
        void SnapCorrect(bool isPlayer, int collectCount);
        void SnapIncorrect(int collectCount);
        void StatusUpdate(Status status, bool inPlay, int playerStackCount, int computerStackCount);
        void TopCard(Card card, bool isPlayerTurn);
        void Welcome();
    }
}