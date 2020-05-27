using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeLibrary.Models
{
    public class GameState : IGameState
    {
        public virtual char[] Board { get; set; }
        public virtual int BoardSize { get { return (int)Math.Sqrt(Board.Length); } }
        public virtual int Turns => Board.Count(x => AllowedChars.Contains(x));
        public virtual char[] AllowedChars { get; set; }
        public char CurrentPlayer => AllowedChars[Turns % 2];
        public bool GameOver { get; set; }
        public virtual ITurnResult TurnResult { get; set; }

        public GameState() { }

        public GameState(int boardSize = 3, char[] allowedChars=null)
        {
            GameOver = false;
            Board = new char[boardSize * boardSize];
        }
    }
}
