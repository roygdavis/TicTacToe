﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Classes
{
    public class Game
    {
        private char[] _board;
        public char[] Board
        {
            get
            {
                return _board;
            }
            set
            {
                _board = value;
            }
        }

        private int _boardSize;
        public readonly char[] AllowedChars = { 'X', 'O' };

        private int _turnsCount;
        public int Turns
        {
            get
            {
                return _turnsCount;
            }
            set
            {
                _turnsCount = value;
            }
        }

        public char CurrentPlayer
        {
            get
            {
                return AllowedChars[_turnsCount % 2];
            }
        }

        public bool GameOver { get; set; }

        public Action<char[]> Render { get; set; }

        Game(int boardSize, Action<char[]> renderAction)
        {
            _boardSize = boardSize;
            Render = renderAction;
            _turnsCount = 0;
            GameOver = false;
            _board = new char[_boardSize * _boardSize];


            Render?.Invoke(Board);
        }

        public static Game CreateInstance(int boardSize, Action<char[]> renderAction)
        {
            if (boardSize < 3 || boardSize % 2 == 0)
            {
                throw new ArgumentException("boardSize must be 3 or more and an odd number");
            }
            return new Game(boardSize, renderAction);
        }

        public GameResult PlayerTurn(int index, char playerChar)
        {
            if (index > (_boardSize * _boardSize) - 1) throw new ArgumentException($"index {index }is bigger than the board size of {_boardSize}");
            if (!AllowedChars.Contains(playerChar)) throw new ArgumentException($"playerChar of {playerChar} is not one of {new string(AllowedChars).Split(",")}");

            _turnsCount++;
            _board[index] = playerChar;
            Render?.Invoke(Board);

            // now test if player won
            if (_turnsCount > 4)
            {
                var diag = new List<char>();
                for (int i = 0; i < _boardSize; i++)
                {
                    var row = _board.Skip(i * _boardSize).Take(_boardSize).ToList();
                    var check = isWinner(row, i, Direction.Row);
                    if (check.HasWinner)
                    {
                        GameOver = true;
                        return check;
                    }

                    var column = new List<char>();
                    for (int c = i; c < _boardSize*_boardSize; c = c + _boardSize)
                    {
                        column.Add(_board[c]);
                    }
                    check = isWinner(column, i, Direction.Column);
                    if (check.HasWinner)
                    {
                        GameOver = true;
                        return check;
                    }

                    if (i == 0)
                        diag.Add(_board[0]);
                    else diag.Add(_board[(i * _boardSize) + i]);
                }

                var diagCheck = isWinner(diag, 0, Direction.Diagonal);
                if (diagCheck.HasWinner)
                {
                    GameOver = true;
                    return diagCheck;
                }
            }
            if (Turns == _boardSize * _boardSize)
            {
                GameOver = true;
            }
            return new GameResult { HasWinner = false, Winner = null, WinningDirection = Direction.None, WinningIndex = null };
        }

        private GameResult isWinner(List<char> lineToCheck, int index, Direction direction)
        {
            foreach (var item in AllowedChars)
            {
                var c = lineToCheck.Where(x => x == item);
                if (c.Count() == _boardSize) return new GameResult() { HasWinner = true, Winner = item, WinningDirection = direction, WinningIndex = index };
            }
            return new GameResult() { HasWinner = false, Winner = new char?(), WinningDirection = Direction.None, WinningIndex = new int?() };
        }
    }
}