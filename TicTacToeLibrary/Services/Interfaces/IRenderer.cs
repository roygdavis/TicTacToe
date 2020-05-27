using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface IRenderer
    {
        void RenderBoard(IGameState gameState);
        bool RenderEnd(IGameState gameState);
        IGameState RenderStart();
        IGameState RenderTurn(IGameState gameState);
    }
}
