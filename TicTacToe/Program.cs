using System;
using TicTacToe.Classes;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What size board do you want to play (enter between 3-9): ");
            var boardSizeChar = Console.ReadKey();
            int boardSize;
            if (Int32.TryParse(boardSizeChar.KeyChar.ToString(), out boardSize))
            {
                Console.WriteLine();
                var game = Game.CreateInstance(boardSize, (c) =>
                  {
                      for (int i = 0; i < c.Length; i++)
                      {
                          var ch = c[i];
                          if (ch == ' ' || ch == '\0') Console.Write("-");
                          else Console.Write(ch);
                          if (i % boardSize == 2) Console.WriteLine();
                      }
                      Console.WriteLine();
                  });

                var nowinner = true;
                while (!game.GameOver)
                {
                    Console.WriteLine($"It's {game.CurrentPlayer} turn, enter your index: ");
                    var indexChars = Console.ReadLine();
                    int index;
                    if (Int32.TryParse(indexChars, out index))
                    {
                        var result = game.PlayerTurn(index, game.CurrentPlayer);
                        if (result.HasWinner)
                        {
                            Console.WriteLine($"Congratulations {result.Winner}, you've won at index {result.WinningIndex} in a {result.WinningDirection} direction!");
                            nowinner = false;
                        }
                    }
                }
                if (nowinner) Console.WriteLine("No one won that game");
            }
            else
            {
                Console.WriteLine("Sorry, please try again with a valid number");
            }
        }
    }
}
