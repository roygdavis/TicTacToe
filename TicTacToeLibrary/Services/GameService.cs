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

            // now test if player won
            if (gameState.Turns > 4)
            {
                var topleftDiag = new List<char>();
                var toprightDiag = new List<char>();
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
                        {
                            topleftDiag.Add(gameState.Board[0]);
                            toprightDiag.Add(gameState.Board[gameState.BoardSize - 1]);
                        }
                        else
                        {
                            topleftDiag.Add(gameState.Board[(i * gameState.BoardSize) + i]);
                            toprightDiag.Add(gameState.Board[((gameState.BoardSize * i) + (gameState.BoardSize - i) - 1)]);
                        }
                    }
                }

                var diagtopleftCheck = isWinner(gameState, topleftDiag, 0, Direction.TopLeftDiagonal);
                if (diagtopleftCheck.HasWinner)
                {
                    return returnGameWon(gameState, diagtopleftCheck);
                }

                var diagtoprightCheck = isWinner(gameState, toprightDiag, 0, Direction.TopRightDiagonal);
                if (diagtoprightCheck.HasWinner)
                {
                    return returnGameWon(gameState, diagtoprightCheck);
                }
            }
            if (gameState.Turns == gameState.BoardSize * gameState.BoardSize)
            {
                gameState.GameOver = true;
                gameState.TurnResult = new TurnResult { HasWinner = false, WinningDirection = Direction.None };
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