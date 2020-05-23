using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Classes
{
    public class Game
    {
        private int[] board;
        private int _boardSize;

        Game(int boardSize)
        {
            _boardSize = boardSize;
        }

        public static Game CreateGame(int boardSize)
        {
            if (boardSize < 3 || boardSize % 2==0)
            {
                throw new ArgumentException("boardSize must be 3 or more and an odd number");
            }
            return new Game(boardSize);
        }

        private void initBoard()
        {
            board = new int[_boardSize];
        }
    }
}
