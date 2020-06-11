using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Models;
using TicTacToeLibrary.Services;

namespace TicTacToe
{
    public class ConsoleRenderer : IRenderer
    {
        private readonly IGameService _gameService;
        private readonly IList<IPlayerService> _playerServices;

        public ConsoleRenderer(IGameService service, IList<IPlayerService> playerServices)
        {
            _gameService = service;
            _playerServices = playerServices;
        }

        public void RenderBoard(IGameState gameState)
        {
            Console.Clear();
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
                        var g = _gameService.CreateState(boardSize, null);
                        RenderBoard(g);
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
            try
            {
                gameState = _gameService.PlayerTurn(gameState, _playerServices.First(x => x.ServiceForPlayer == gameState.CurrentPlayer).GetPlayerMoveIndex(gameState));
                RenderBoard(gameState);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return gameState;
        }

        private void renderWin(IGameState gameState)
        {
            Console.WriteLine($"Congratulations {gameState.TurnResult.Winner});");
            foreach (var item in gameState.TurnResult.WinningDetails)
            {
                Console.WriteLine($"\tyou've won at index {item.WinningIndex} in a {item.WinningDirection} direction!");
            }
        }
    }
}
