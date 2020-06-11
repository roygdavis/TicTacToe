using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public class NextMoveOnlyAIService : IPlayerService
    {
        private readonly IGameService _gameService;


        public NextMoveOnlyAIService(char playerChar, IGameService gameService)
        {
            ServiceForPlayer = playerChar;
            _gameService = gameService;
        }

        public char ServiceForPlayer { get; private set; }

        public int GetPlayerMoveIndex(IGameState gameState)
        {
            return DetermineNextBestMove(gameState);
        }

        private int DetermineNextBestMove(IGameState initialGameState)
        {
            // make a copy of the current game state
            IGameState workingState = new GameState
            {
                Board = (char[])initialGameState.Board.Clone(),
                AllowedChars = (char[])initialGameState.AllowedChars.Clone(),
                GameOver = initialGameState.GameOver,
                TurnResult = new TurnResult
                {
                    WinningDetails = new List<IWinningDetails>()
                }
            };

            // go through each item and return how many turns away from a win

            // the space with the smallest turns count is the spot to go for

            // -1 means we lose
            // 0 means we draw
            // >0 means we win - smaller the better

            var emptyIndexes = workingState.Board.Select((c, i) => workingState.AllowedChars.Contains(c) ? -1 : i);
            foreach (var index in emptyIndexes.Where(x => x > -1))
            {
                workingState = _gameService.PlayerTurn(workingState, index);
                if (workingState.GameOver && workingState.TurnResult.HasWinner && workingState.TurnResult.Winner == ServiceForPlayer) return index;
                if (workingState.GameOver && workingState.TurnResult.HasWinner && workingState.TurnResult.Winner != ServiceForPlayer) return -1;
                if (workingState.GameOver && workingState.TurnResult.HasWinner == false) return 0;
            }
            return emptyIndexes.First();
        }
    }
}
