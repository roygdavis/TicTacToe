using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Models
{
    public interface IWinningDetails
    {
        Direction WinningDirection { get; set; }
        int WinningIndex { get; set; }
    }
}
