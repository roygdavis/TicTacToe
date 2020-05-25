using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Classes
{
    public class ConsoleRenderer : IRenderer
    {
        public void Render(IGameDetails gameDetails)
        {

            Console.WriteLine();
            for (int i = 0; i < gameDetails.Board.Length; i++)
            {
                var ch = gameDetails.Board[i];
                if (ch == ' ' || ch == '\0') Console.Write("-");
                else Console.Write(ch);
                if (i % gameDetails.BoardSize == gameDetails.BoardSize - 1) Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void RenderDraw()
        {
            Console.WriteLine("No-one won :-(");
        }

        public bool RenderEnd()
        {
            Console.WriteLine("Thanks for playing!");
            return false; // TODO: return true to play again
        }

        public IGameDetails RenderStart()
        {
            bool sizeParsed = false;
            while (!sizeParsed)
            {
                Console.WriteLine("What size board do you want to play (enter between 3-9): ");
                var boardSizeChar = Console.ReadKey();
                int boardSize;
                sizeParsed = Int32.TryParse(boardSizeChar.KeyChar.ToString(), out boardSize);
                if (sizeParsed)
                {
                    // TODO: get players to choose their characters!
                    Console.WriteLine();
                    return new GameDetails(boardSize);
                }
                else
                {
                    Console.WriteLine("I didn't understand that. Please try again");
                }
            }
            return null;
        }

        public PlayerTurn RenderTurn(IGameDetails gameDetails)
        {
            bool indexParsed = false;
            while (!indexParsed)
            {
                Console.WriteLine($"It's {gameDetails.CurrentPlayer} turn, enter your index (0-{gameDetails.Board.Length - 1}): ");
                var indexChars = Console.ReadLine();
                int index;
                indexParsed = Int32.TryParse(indexChars, out index);
                if (indexParsed)
                {
                    if (index < 0 || index > gameDetails.Board.Length) Console.WriteLine($"Index {index} is not valid");
                    else if (gameDetails.AllowedChars.Contains(gameDetails.Board[index])) Console.WriteLine("That space is already taken");
                    else return new PlayerTurn { Index = index, PlayerChar = gameDetails.CurrentPlayer };
                    indexParsed = false;
                }
            }
            return null;
        }

        public void RenderWin(IGameResult gameResult)
        {
            if (gameResult.HasWinner)
            {
                Console.WriteLine($"Congratulations {gameResult.Winner}, you've won at index {gameResult.WinningIndex} in a {gameResult.WinningDirection} direction!");
            }
        }
    }
}
