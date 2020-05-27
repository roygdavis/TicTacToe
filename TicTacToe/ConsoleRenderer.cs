using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Models;
using TicTacToeLibrary.Services;

namespace TicTacToe
{
    public class ConsoleRenderer : IConsoleRenderer
    {
        private readonly IGameService gameService;

        public ConsoleRenderer(IGameService service)
        {
            gameService = service;
        }

        public void RenderBoard(IGameState gameState)
        {

            Console.WriteLine();
            for (int i = 0; i < gameState.Board.Length; i++)
            {
                var ch = gameState.Board[i];
                if (ch == ' ' || ch == '\0') Console.Write("-");
                else Console.Write(ch);
                if (i % gameState.BoardSize == gameState.BoardSize - 1) Console.WriteLine();
            }
            Console.WriteLine();
        }

        private void renderDraw()
        {
            Console.WriteLine("No-one won :-(");
        }

        public bool RenderEnd(IGameState gameState)
        {
            if (gameState.GameOver && gameState.TurnResult.HasWinner)
            {
                renderWin(gameState);
            }
            else
            {
                renderDraw();
            }
            Console.WriteLine("Thanks for playing!");
            return false; // TODO: return true to play agian
        }

        public IGameState RenderStart()
        {
            bool sizeParsed = false;
            while (!sizeParsed)
            {
                Console.WriteLine("What size board do you want to play (must be an odd number and 3 or more): ");
                var boardSizeChar = Console.ReadLine();
                int boardSize;
                sizeParsed = Int32.TryParse(boardSizeChar, out boardSize);
                if (sizeParsed)
                {
                    try
                    {
                        Console.WriteLine();
                        var g = gameService.CreateState(boardSize, null);
                        return g;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        sizeParsed = false;
                    }
                }
                else
                {
                    Console.WriteLine("I didn't understand that. Please try again");
                }
            }
            return new GameState();
        }

        public IGameState RenderTurn(IGameState gameState)
        {
            bool indexParsed = false;
            while (!indexParsed)
            {
                RenderBoard(gameState);
                Console.WriteLine($"It's {gameState.CurrentPlayer} turn, enter your index (0-{gameState.Board.Length - 1}): ");
                var indexChars = Console.ReadLine();
                int index;
                indexParsed = Int32.TryParse(indexChars, out index);
                if (indexParsed)
                {
                    try
                    {
                        gameState = gameService.PlayerTurn(gameState, index);
                        return gameState;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        indexParsed = false;
                    }
                }
            }
            return null;
        }

        private void renderWin(IGameState gameState)
        {
            Console.WriteLine($"Congratulations {gameState.TurnResult.Winner}, you've won at index {gameState.TurnResult.WinningIndex} in a {gameState.TurnResult.WinningDirection} direction!");
        }
    }
}
