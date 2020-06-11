using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface IPlayerService
    {
        char ServiceForPlayer { get; }
        int GetPlayerMoveIndex(IGameState gameState);
    }
}
