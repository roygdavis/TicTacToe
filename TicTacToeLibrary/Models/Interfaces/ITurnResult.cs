using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Models
{
    public interface ITurnResult
    {
        bool HasWinner { get; set; }
        char? Winner { get; set; }
        Direction WinningDirection { get; set; }
        int? WinningIndex { get; set; }
    }
}