namespace TicTacToeLibrary.Models
{
    public interface IGameState
    {
        char[] Board { get; set; }
        char CurrentPlayer { get; }
        bool GameOver { get; set; }
        int Turns { get; }
        int BoardSize { get; }
        char[] AllowedChars { get; }
        ITurnResult TurnResult { get; set; }
    }
}