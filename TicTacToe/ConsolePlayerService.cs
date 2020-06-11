using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Models;
using TicTacToeLibrary.Services;

namespace TicTacToe
{
    class ConsolePlayerService : IPlayerService
    {
        public char ServiceForPlayer { get; private set; }

        public ConsolePlayerService(char playerChar) => ServiceForPlayer = playerChar;

        public int GetPlayerMoveIndex(IGameState gameState)
        {
            bool indexParsed = false;
            while (!indexParsed)
            {
                Console.WriteLine($"It's {gameState.CurrentPlayer} turn, enter your index (0-{gameState.Board.Length - 1}): ");
                var indexChars = Console.ReadLine();
                int index;
                indexParsed = Int32.TryParse(indexChars, out index);
                if (indexParsed)
                {
                    return index;
                }
            }
            return -1;
        }
    }
}
