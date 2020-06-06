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

            gameState = isMoveAWinner(gameState);
            
            if (gameState.TurnResult != null && gameState.TurnResult.HasWinner) return gameState;
            if (gameState.Turns == gameState.BoardSize * gameState.BoardSize)
            {
                gameState.GameOver = true;
                gameState.TurnResult = new TurnResult { HasWinner = false, WinningDirection = Direction.None };
            }
            return gameState;
        }

        private IGameState isMoveAWinner(IGameState gameState)
        {
            gameState.TurnResult = new TurnResult
            {
                HasWinner = false,
                Winner = null,
                WinningDirection = Direction.None,
                WinningIndex = null
            };
            // now test if player won
            if (gameState.Turns > 4)
            {
                var topleftDiag = new List<char>();
                var toprightDiag = new List<char>();
                for (int i = 0; i < gameState.BoardSize; i++)
                {
                    var row = gameState.Board.Skip(i * gameState.BoardSize).Take(gameState.BoardSize).ToList();
                    if (isWinningLine(gameState, row))
                    {
                        gameState.TurnResult = new TurnResult
                        {
                            HasWinner = true,
                            Winner = row.First(),
                            WinningDirection = Direction.Row,
                            WinningIndex = i
                        };
                    }
                    else
                    {
                        var column = new List<char>();
                        for (int c = i; c < gameState.BoardSize * gameState.BoardSize; c = c + gameState.BoardSize)
                        {
                            column.Add(gameState.Board[c]);
                        }
                        if (isWinningLine(gameState, column))
                        {
                            gameState.TurnResult = new TurnResult
                            {
                                HasWinner = true,
                                Winner = column.First(),
                                WinningDirection = Direction.Column,
                                WinningIndex = i
                            };
                        }
                        else
                        {
                            if (i == 0)
                            {
                                topleftDiag.Add(gameState.Board[0]);
                                toprightDiag.Add(gameState.Board[gameState.BoardSize - 1]);
                            }
                            else
                            {
                                topleftDiag.Add(gameState.Board[(i * gameState.BoardSize) + i]);
                                toprightDiag.Add(gameState.Board[((gameState.BoardSize * i) + (gameState.BoardSize - i) - 1)]);
                            }
                            if (isWinningLine(gameState, topleftDiag))
                            {
                                gameState.TurnResult = new TurnResult
                                {
                                    HasWinner = true,
                                    Winner = topleftDiag.First(),
                                    WinningDirection = Direction.TopLeftDiagonal,
                                    WinningIndex = 0
                                };
                            }
                            else
                            {
                                if (isWinningLine(gameState, toprightDiag))
                                {
                                    gameState.TurnResult = new TurnResult
                                    {
                                        HasWinner = true,
                                        Winner = toprightDiag.First(),
                                        WinningDirection = Direction.TopRightDiagonal,
                                        WinningIndex = gameState.BoardSize - 1
                                    };
                                }
                            }
                        }
                    }
                }
            }
            return gameState;
        }

        private bool isWinningLine(IGameState gameState, List<Char> lineToCheck)
        {
            return lineToCheck.Count(x => x == gameState.AllowedChars.First()) == gameState.BoardSize || lineToCheck.Count(x => x == gameState.AllowedChars.Last()) == gameState.BoardSize;
        }        
    }
}