using System;
using TicTacToeLibrary.Models;
using TicTacToeLibrary.Services;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameService = new GameService();
            var consoleRenderer = new ConsoleRenderer(gameService);
            var gameState = consoleRenderer.RenderStart();
            while (!gameState.GameOver)
            {
                gameState = consoleRenderer.RenderTurn(gameState);
            }            
        }
    }
}
