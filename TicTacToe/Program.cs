using TicTacToe.Services;

namespace TicTacToe
{
    public class Program
    {
        static void Main(string[] args)
        {
            var renderer = new RenderingService();
            var game = new GameService(renderer);

            game.Play();
        }

        
    }
}