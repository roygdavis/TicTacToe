using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeLibrary.Models
{
    public class GameState : IGameState
    {
        public virtual char[] Board { get; set; }
        public virtual int BoardSize { get { return (int)Math.Sqrt(Board.Length); } }
        public virtual int Turns => Board.Count(x => AllowedChars.Contains(x));
        public virtual char[] AllowedChars { get; set; }
        public char CurrentPlayer
        {
            get
            {
                var distinctCounts = Board.GroupBy(x => x).Where(x => AllowedChars.Contains(x.Key)).Select(x => new { Key = x.Key, Total = x.Count() }).OrderBy(x => x.Total);
                if (distinctCounts.Count() == 1) return AllowedChars.First(x => x != distinctCounts.First().Key);
                if (distinctCounts.Select(x => x.Total).Distinct().Count() > 1) return distinctCounts.First().Key;
                return AllowedChars.First(); //AllowedChars[Turns % 2];
            }
        }
        public bool GameOver { get; set; }
        public virtual ITurnResult TurnResult { get; set; }
    }
}
