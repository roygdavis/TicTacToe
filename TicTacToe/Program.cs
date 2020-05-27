using System;
using TicTacToeLibrary.Services;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleRenderer = new ConsoleRenderer();
            var game = GameService.CreateInstance(consoleRenderer);
            game.Play();

            //    var nowinner = true;
            //    while (!game.GameOver)
            //    {
            //        
            //    }
            //    if (nowinner) Console.WriteLine("No one won that game");
            //}
            //else
            //{
            //    Console.WriteLine("Sorry, please try again with a valid number");
            //}
        }
    }
}
