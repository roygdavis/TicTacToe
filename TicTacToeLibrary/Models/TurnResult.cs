using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Models
{
    public class TurnResult : ITurnResult
    {
        public Nullable<char> Winner { get; set; }
        public bool HasWinner { get { return WinningDetails == null || WinningDetails.Count > 0; } }
        public IList<IWinningDetails> WinningDetails { get; set; }
    }
}
