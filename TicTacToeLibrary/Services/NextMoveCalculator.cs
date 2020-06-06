using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public class NextMoveCalculator : INextMoveCalculator
    {
        public int DetermineNextBestMoveIndex(IGameState gameState)
        {
            return DetermineNextBestMove(gameState, 0, gameState.CurrentPlayer);
        }

        private int DetermineNextBestMove(IGameState initialGameState, int depth, char aiChar)
        {
            var gameState = new GameState
            {
                Board = (char[])initialGameState.Board.Clone(),
                AllowedChars = (char[])initialGameState.AllowedChars.Clone(),
                GameOver = initialGameState.GameOver
            };

            for (int i = 0; i < gameState.Board.Length; i++)
            {
                if (!gameState.AllowedChars.Contains(gameState.Board[i]))
                {
                    gameState.Board[i] = gameState.CurrentPlayer;
                    //var nextMoveState = isMoveAWinner(gameState);
                    //if (nextMoveState.TurnResult != null && nextMoveState.TurnResult.HasWinner && nextMoveState.TurnResult.Winner == aiChar) return 1;
                    //if (nextMoveState.TurnResult != null && nextMoveState.TurnResult.HasWinner && nextMoveState.TurnResult.Winner != aiChar) return -1;
                    //if (nextMoveState.TurnResult != null && !nextMoveState.TurnResult.HasWinner) return 0;
                    //return DetermineNextBestMove(nextMoveState, depth++, aiChar);
                }
            }
            return 0;
        }
    }
}
