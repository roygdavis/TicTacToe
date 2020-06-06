using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface INextMoveCalculator
    {
        int DetermineNextBestMoveIndex(IGameState gameState);
    }
}
