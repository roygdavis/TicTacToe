using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface IGameService
    {
        IGameState CreateState(int boardSize, char[] playerChars);
        IGameState PlayerTurn(IGameState gameDetails, int index);
    }
}