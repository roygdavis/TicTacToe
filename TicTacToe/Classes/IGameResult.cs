namespace TicTacToe.Classes
{
    public interface IGameResult
    {
        bool HasWinner { get; set; }
        char? Winner { get; set; }
        Direction WinningDirection { get; set; }
        int? WinningIndex { get; set; }
    }
}