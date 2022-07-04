using System;

namespace TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            DrawBoard();
        }

        static void DrawBoard()
        {
            Console.WriteLine("+---+---+---+");
            for (int x = 0; x < 3; x++)
            {
                Console.Write('|');
                for (int y = 0; y < 3; y++)
                {
                    Console.Write("   |");
                }
                Console.WriteLine();
                Console.WriteLine("+---+---+---+");
            }
        }
    }
}