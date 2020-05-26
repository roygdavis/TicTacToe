namespace TicTacToe.Classes
{
    public interface IGameDetails
    {
        char[] Board { get; set; }
        char CurrentPlayer { get; }
        bool GameOver { get; set; }
        int Turns { get; set; }
        int BoardSize { get; set; }
        char[] AllowedChars { get; }
    }
}