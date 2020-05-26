using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Classes
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
