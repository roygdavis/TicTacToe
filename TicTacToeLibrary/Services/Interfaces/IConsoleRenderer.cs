using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface IConsoleRenderer
    {
        void RenderBoard(IGameState gameState);
        bool RenderEnd(IGameState gameState);
        IGameState RenderStart();
        IGameState RenderTurn(IGameState gameState);
    }
}