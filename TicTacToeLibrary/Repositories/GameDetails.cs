using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToeLibrary.Repositories
{
    public class GameDetails : IGameDetails
    {
        public char[] Board { get; set; }
        public int Turns { get; set; }

        public int BoardSize { get; set; }
        public char[] AllowedChars { get; set; }


        public char CurrentPlayer => AllowedChars[Turns % 2];


        public bool GameOver { get; set; }

        public GameDetails(int boardSize = 3, char[] allowedChars=null)
        {
            if (allowedChars == null) AllowedChars = new char[] { 'X', 'O' };
            BoardSize = boardSize;
            Turns = 0;
            GameOver = false;
            Board = new char[BoardSize * BoardSize];
        }
    }
}
