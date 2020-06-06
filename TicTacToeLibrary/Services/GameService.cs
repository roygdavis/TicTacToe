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
        public IGameState CreateState(int boardSize = 3, char[] playerChars = null)
        {
            if (boardSize < 3 || boardSize % 2 == 0)
            {
                throw new ArgumentException("boardSize must be 3 or more and an odd number");
            }
            if (playerChars == null) playerChars = new char[] { 'X', 'O' };
            if (playerChars.Length < 2 || playerChars.Length > 2) throw new ArgumentException("playerChars can only be a length of 2");
            if (playerChars.Distinct().Count() == 1) throw new ArgumentException("Each playerChar must be a unique character");
            return new GameState() { AllowedChars = playerChars, Board = new char[boardSize * boardSize] };
        }

        public IGameState PlayerTurn(IGameState gameState, int index)
        {
            if (index > (gameState.BoardSize * gameState.BoardSize) - 1) throw new ArgumentException($"index {index} is bigger than the board size of {gameState.BoardSize}");
            if (gameState.Board.Count(x => gameState.AllowedChars.Contains(x)) >= gameState.Board.Length) throw new NotSupportedException("Game is over, there are no spaces left on the board.  Start a new game.");
            if (gameState.AllowedChars.Contains(gameState.Board[index])) throw new ArgumentException("That space has already been taken");

            gameState.Board[index] = gameState.CurrentPlayer;

            return isMoveAWinner(gameState);            
        }

        private IGameState isMoveAWinner(IGameState gameState)
        {
            gameState.TurnResult = new TurnResult
            {
                WinningDetails = new List<IWinningDetails>()
            };
            // now test if player won
            if (gameState.Turns > 4)
            {
                gameState.GameOver = true;

                // diagonals
                var topLeftDiagonal = gameState.Board.Select((c, i) => i % (gameState.BoardSize + 1) == 0 ? c : ' ').Where(x => x != ' ').ToList();
                if (isWinningLine(gameState, topLeftDiagonal))
                {
                    gameState.TurnResult.Winner = topLeftDiagonal.First();
                    gameState.TurnResult.WinningDetails.Add(new WinningDetails
                    {
                        WinningDirection = Direction.TopLeftDiagonal,
                        WinningIndex = 0
                    });
                }

                var topRightDiagonal = gameState.Board.Select((c, i) => i != 0 && i % (gameState.BoardSize - 1) == 0 && i != (gameState.BoardSize * gameState.BoardSize - 1) ? c : ' ').Where(x => x != ' ').ToList();
                if (isWinningLine(gameState, topRightDiagonal))
                {
                    gameState.TurnResult.Winner = topRightDiagonal.First();
                    gameState.TurnResult.WinningDetails.Add(new WinningDetails
                    {
                        WinningDirection = Direction.TopRightDiagonal,
                        WinningIndex = gameState.BoardSize - 1
                    });
                }

                // rows & columns
                for (int i = 0; i < gameState.BoardSize; i++)
                {
                    var row = gameState.Board.Skip(i * gameState.BoardSize).Take(gameState.BoardSize).ToList();
                    if (isWinningLine(gameState, row))
                    {
                        gameState.TurnResult.Winner = row.First();
                        gameState.TurnResult.WinningDetails.Add(new WinningDetails
                        {
                            WinningDirection = Direction.Row,
                            WinningIndex = i
                        });
                    }

                    var column = new List<char>();
                    for (int c = i; c < gameState.BoardSize * gameState.BoardSize; c = c + gameState.BoardSize)
                    {
                        column.Add(gameState.Board[c]);
                    }
                    if (isWinningLine(gameState, column))
                    {
                        gameState.TurnResult.Winner = column.First();
                        gameState.TurnResult.WinningDetails.Add(new WinningDetails
                        {
                            WinningDirection = Direction.Column,
                            WinningIndex = i
                        });
                    }
                }
            }
            return gameState;
        }

        private bool isWinningLine(IGameState gameState, List<Char> lineToCheck)
        {
            return lineToCheck.Distinct().Count() == 1 && lineToCheck.Count(x => gameState.AllowedChars.Contains(x)) == gameState.BoardSize;
        }
    }
}