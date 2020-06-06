using System.Collections.Generic;
using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Models
{
    public interface ITurnResult
    {
        bool HasWinner { get; }
        char? Winner { get; set; }
        IList<IWinningDetails> WinningDetails { get; set; }
    }
}