using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Classes
{
    public class GameResult
    {
        public Nullable<int> WinningIndex { get; set; }
        public Direction WinningDirection { get; set; }
        public Nullable<char> Winner { get; set; }
        public bool HasWinner { get; set; }
    }
}
