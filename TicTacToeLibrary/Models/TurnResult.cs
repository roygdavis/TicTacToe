using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Models
{
    public class TurnResult : ITurnResult
    {
        public Nullable<int> WinningIndex { get; set; }
        public Direction WinningDirection { get; set; }
        public Nullable<char> Winner { get; set; }
        public bool HasWinner { get; set; }
    }
}
