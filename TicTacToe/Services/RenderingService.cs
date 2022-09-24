using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Services
{
    public class RenderingService : IRenderingService
    {
        public void DrawBoard(List<int> noughts, List<int> crosses)
        {
            Console.WriteLine("+---+---+---+");
            int gridPosition = 1;
            for (int x = 0; x < 3; x++)
            {
                Console.Write('|');
                for (int y = 0; y < 3; y++)
                {
                    if (!(noughts.Contains(gridPosition) || crosses.Contains(gridPosition)))
                    {
                        Console.Write("   |");
                    }
                    if (noughts.Contains(gridPosition))
                    {
                        Console.Write(" O |");
                    }
                    if (crosses.Contains(gridPosition))
                    {
                        Console.Write(" X |");
                    }
                    gridPosition++;
                }
                Console.WriteLine();
                Console.WriteLine("+---+---+---+");
            }
        }
    }
}
