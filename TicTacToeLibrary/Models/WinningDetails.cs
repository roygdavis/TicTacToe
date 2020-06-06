using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Enums;

namespace TicTacToeLibrary.Models
{
    public class WinningDetails : IWinningDetails
    {
        public Direction WinningDirection { get; set; }
        public int WinningIndex { get; set; }
    }
}
