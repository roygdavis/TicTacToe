using System.Collections.Generic;

namespace TicTacToe.Services
{
    public interface IRenderingService
    {
        void DrawBoard(List<int> noughts, List<int> crosses);
    }
}