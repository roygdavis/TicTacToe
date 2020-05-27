using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Repositories
{
    public interface IGameResult
    {
        bool HasWinner { get; set; }
        char? Winner { get; set; }
        Direction WinningDirection { get; set; }
        int? WinningIndex { get; set; }
    }
}