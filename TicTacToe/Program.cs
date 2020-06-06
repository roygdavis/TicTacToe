using Microsoft.Extensions.DependencyInjection;
using System;
using TicTacToeLibrary.Models;
using TicTacToeLibrary.Services;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IGameService, GameService>()
                .AddSingleton<IRenderer, ConsoleRenderer>()
                .BuildServiceProvider();

            var consoleRenderer = serviceProvider.GetService<IRenderer>();

            var gameState = consoleRenderer.RenderStart();
            while (!gameState.GameOver)
            {
                gameState = consoleRenderer.RenderTurn(gameState);
            }
            consoleRenderer.RenderEnd(gameState);
        }
    }
}
