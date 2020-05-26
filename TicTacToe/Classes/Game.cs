using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Classes
{
    public class Game
    {
        public IGameDetails GameDetails { get; set; }

        public IRenderer Renderer { get; set; }

        public Game()
        {
            GameDetails = new GameDetails();
        }

        Game(IGameDetails gameDetails, IRenderer renderer)
        {
            GameDetails = gameDetails;
            Renderer = renderer;
            Renderer?.Render(gameDetails);
        }

        public static Game CreateInstance(IRenderer renderer)
        {
            var gameDetails = renderer.RenderStart();
            if (gameDetails.BoardSize < 3 || gameDetails.BoardSize % 2 == 0)
            {
                throw new ArgumentException("boardSize must be 3 or more and an odd number");
            }
            return new Game(gameDetails, renderer);
        }

        public void Play()
        {
            while(!GameDetails.GameOver)
            {
                PlayerTurn pt = Renderer.RenderTurn(GameDetails);
                var gameResult = PlayerTurn(pt.Index, pt.PlayerChar);
                if (gameResult.HasWinner)
                {
                    Renderer?.RenderWin(gameResult);
                }
            }
            Renderer?.RenderEnd();
        }

        public GameResult PlayerTurn(int index, char playerChar)
        {
            if (index > (GameDetails.BoardSize * GameDetails.BoardSize) - 1) throw new ArgumentException($"index {index} is bigger than the board size of {GameDetails.BoardSize}");
            if (!GameDetails.AllowedChars.Contains(playerChar)) throw new ArgumentException($"playerChar of {playerChar} is not one of {new string(GameDetails.AllowedChars).Split(",")}");

            GameDetails.Turns++;
            GameDetails.Board[index] = playerChar;
            Renderer?.Render(GameDetails);

            // now test if player won
            if (GameDetails.Turns > 4)
            {
                var diag = new List<char>();
                for (int i = 0; i < GameDetails.BoardSize; i++)
                {
                    var row = GameDetails.Board.Skip(i * GameDetails.BoardSize).Take(GameDetails.BoardSize).ToList();
                    var check = isWinner(row, i, Direction.Row);
                    if (check.HasWinner)
                    {
                        GameDetails.GameOver = true;
                        return check;
                    }

                    var column = new List<char>();
                    for (int c = i; c < GameDetails.BoardSize*GameDetails.BoardSize; c = c + GameDetails.BoardSize)
                    {
                        column.Add(GameDetails.Board[c]);
                    }
                    check = isWinner(column, i, Direction.Column);
                    if (check.HasWinner)
                    {
                        GameDetails.GameOver = true;
                        return check;
                    }

                    if (i == 0)
                        diag.Add(GameDetails.Board[0]);
                    else diag.Add(GameDetails.Board[(i * GameDetails.BoardSize) + i]);
                }

                var diagCheck = isWinner(diag, 0, Direction.Diagonal);
                if (diagCheck.HasWinner)
                {
                    GameDetails.GameOver = true;
                    return diagCheck;
                }
            }
            if (GameDetails.Turns == GameDetails.BoardSize * GameDetails.BoardSize)
            {
                GameDetails.GameOver = true;
            }
            return new GameResult { HasWinner = false, Winner = null, WinningDirection = Direction.None, WinningIndex = null };
        }

        private GameResult isWinner(List<char> lineToCheck, int index, Direction direction)
        {
            foreach (var item in GameDetails.AllowedChars)
            {
                var c = lineToCheck.Where(x => x == item);
                if (c.Count() == GameDetails.BoardSize) return new GameResult() { HasWinner = true, Winner = item, WinningDirection = direction, WinningIndex = index };
            }
            return new GameResult() { HasWinner = false, Winner = new char?(), WinningDirection = Direction.None, WinningIndex = new int?() };
        }
    }
}