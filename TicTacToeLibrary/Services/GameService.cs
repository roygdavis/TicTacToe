using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Enums;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public class GameService : IGameService
    {
        public IRenderer Renderer { get; set; }

        GameService(IRenderer renderer)
        {
            Renderer = renderer;
        }

        public static GameService CreateInstance(IRenderer renderer)
        {
            if (renderer == null) throw new ArgumentException("renderer cannot be null");
            return new GameService(renderer);
        }

        public void Play()
        {
            var gameState = Renderer.RenderStart();
            if (gameState.BoardSize < 3 || gameState.BoardSize % 2 == 0)
            {
                // TODO: move this constraint out of this service
                // or create a service around gameDetails
                throw new ArgumentException("boardSize must be 3 or more and an odd number");
            }
            Renderer.Render(gameState);
            while (!gameState.GameOver)
            {
                PlayerTurn pt = Renderer.RenderTurn(gameState);
                var gameResult = PlayerTurn(gameState, pt.Index, pt.PlayerChar);
                if (gameResult.TurnResult.HasWinner)
                {
                    Renderer?.RenderWin(gameResult.TurnResult);
                }
            }
            Renderer.RenderEnd();
        }

        public IGameState PlayerTurn(IGameState gameState, int index, char playerChar)
        {
            if (index > (gameState.BoardSize * gameState.BoardSize) - 1) throw new ArgumentException($"index {index} is bigger than the board size of {gameState.BoardSize}");
            if (!gameState.AllowedChars.Contains(playerChar)) throw new ArgumentException($"playerChar of {playerChar} is not one of {new string(gameState.AllowedChars).Split(",")}");
            if (gameState.Board.Count(x => gameState.AllowedChars.Contains(x)) >= gameState.Board.Length) throw new NotSupportedException("Game is over, there are no spaces left on the board.  Start a new game.");
            if (gameState.AllowedChars.Contains(gameState.Board[index])) throw new ArgumentException("That space has already been taken");

            gameState.Board[index] = playerChar;
            Renderer.Render(gameState);

            // now test if player won
            if (gameState.Turns > 4)
            {
                var diag = new List<char>();
                for (int i = 0; i < gameState.BoardSize; i++)
                {
                    var row = gameState.Board.Skip(i * gameState.BoardSize).Take(gameState.BoardSize).ToList();
                    var check = isWinner(gameState, row, i, Direction.Row);
                    if (!check.HasWinner)
                    {
                        var column = new List<char>();
                        for (int c = i; c < gameState.BoardSize * gameState.BoardSize; c = c + gameState.BoardSize)
                        {
                            column.Add(gameState.Board[c]);
                        }
                        check = isWinner(gameState, column, i, Direction.Column);
                    }

                    if (check.HasWinner)
                    {
                        return returnGameWon(gameState, check);
                    }
                    else
                    {
                        if (i == 0)
                            diag.Add(gameState.Board[0]);
                        else diag.Add(gameState.Board[(i * gameState.BoardSize) + i]);
                    }
                }

                var diagCheck = isWinner(gameState, diag, 0, Direction.Diagonal);
                if (diagCheck.HasWinner)
                {
                    return returnGameWon(gameState, diagCheck);
                }
            }
            if (gameState.Turns == gameState.BoardSize * gameState.BoardSize)
            {
                gameState.GameOver = true;
            }
            return gameState;
        }

        private IGameState returnGameWon(IGameState gameState, ITurnResult result)
        {
            if (result.HasWinner) gameState.GameOver = true;
            else gameState.GameOver = false;
            gameState.TurnResult = result;
            return gameState;
        }

        private TurnResult isWinner(IGameState gameState, List<char> lineToCheck, int index, Direction direction)
        {
            foreach (var item in gameState.AllowedChars)
            {
                var c = lineToCheck.Where(x => x == item);
                if (c.Count() == gameState.BoardSize) return new TurnResult() { HasWinner = true, Winner = item, WinningDirection = direction, WinningIndex = index };
            }
            return new TurnResult() { HasWinner = false, Winner = new char?(), WinningDirection = Direction.None, WinningIndex = new int?() };
        }
    }
}