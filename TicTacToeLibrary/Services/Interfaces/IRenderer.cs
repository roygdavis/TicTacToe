using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeLibrary.Services
{
    public interface IRenderer
    {
        void Render(IGameState gameDetails);
        void RenderWin(ITurnResult gameResult);
        IGameState RenderStart();
        PlayerTurn RenderTurn(IGameState gameDetails);
        bool RenderEnd();
        void RenderDraw();
    }
}
