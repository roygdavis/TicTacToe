using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface IGameService
    {
        IRenderer Renderer { get; set; }

        void Play();
        IGameState PlayerTurn(IGameState gameDetails, int index, char playerChar);
    }
}