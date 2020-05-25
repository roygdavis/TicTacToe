using System;
using TicTacToe.Classes;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var consoleRenderer = new ConsoleRenderer();
            var game = Game.CreateInstance(consoleRenderer);
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
