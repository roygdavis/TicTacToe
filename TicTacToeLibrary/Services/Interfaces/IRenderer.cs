using System;
using System.Collections.Generic;
using System.Text;
using TicTacToeLibrary.Repositories;

namespace TicTacToeLibrary.Services
{
    public interface IRenderer
    {
        void Render(IGameDetails gameDetails);
        void RenderWin(IGameResult gameResult);
        IGameDetails RenderStart();
        PlayerTurn RenderTurn(IGameDetails gameDetails);
        bool RenderEnd();
        void RenderDraw();
    }
}
