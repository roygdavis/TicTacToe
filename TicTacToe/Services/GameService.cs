using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Services
{
    public class GameService
    {
        private readonly IRenderingService _renderer;

        public GameService(IRenderingService renderer)
        {
            _renderer = renderer;
        }

        public void Play()
        {
            List<int> noughts = new List<int>();
            List<int> crosses = new List<int>();
            bool isNoughtsTurn = true;

            while (true)
            {
                // draw our board with our players positions
                _renderer.DrawBoard(noughts, crosses);

                // get our player input
                Console.WriteLine("It's your turn {0}", isNoughtsTurn ? "Nought" : "Cross");
                Console.Write("Where do you want to go (1-9)? ");
                string input = Console.ReadLine();

                if (isNoughtsTurn)
                {
                    noughts.Add(int.Parse(input));
                }
                else
                {
                    crosses.Add(int.Parse(input));
                }
                isNoughtsTurn = !isNoughtsTurn;
            }
        }
    }
}
