using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TicTacToe.Classes;

namespace TicTacToeTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        [DataRow(-100)]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(2)]
        public void Game_IfBoardSizeIsLessThan3_ShouldThrowException(int boardSize)
        {
            // Arrange/Act/Assert
            Assert.ThrowsException<ArgumentException>(() => Game.CreateInstance(boardSize, null));
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(100)]
        public void Game_IfBoardSizeNotOddNumber_ShouldThrowException(int boardSize)
        {
            // Arrange/Act/Assert
            Assert.ThrowsException<ArgumentException>(() => Game.CreateInstance(boardSize, null));
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(3)]
        [DataRow(5)]
        public void Game_IfBoardSizeIsValid_ShouldNotThrowException(int boardSize)
        {
            // Arrange/Act
            var o = Game.CreateInstance(boardSize, null);

            //Assert
            Assert.IsTrue(o is Game);
        }

        [TestMethod]
        [DataRow(new char[]
            {
                'X', 'X', ' ',
                'O','O',' ',
                ' ',' ',' '
            }, 2, 0, Direction.Row, 'X')]
        [DataRow(new char[]
            {
                'X', 'X', ' ',
                'O','O',' ',
                ' ',' ',' '
            }, 5, 1, Direction.Row, 'O')]
        [DataRow(new char[]
            {
                'X', 'X', ' ',
                'X','O','X',
                'O',' ','O'
            }, 7, 2, Direction.Row, 'O')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'O','X','O',
                'X','O',' '
            }, 8, 0, Direction.Diagonal, 'X')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'O','X','O',
                'X','O',' '
            }, 8, 2, Direction.Column, 'O')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'O','X','O',
                'X',' ','X'
            }, 7, 1, Direction.Column, 'X')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 3, 0, Direction.Column, 'X')]
        public void Game_PlayerTurn_WhenWinningTurn_HasWinner_ShouldBeTrue(char[] board, int nextMoveIndex, int expectedWinningIndex, Direction expectedDirection, char expectedWinningChar)
        {
            // Arrange
            var sut = Game.CreateInstance((int)Math.Sqrt((double)board.Length), null);
            sut.Board = board;
            sut.Turns = board.Count(x => x != ' ');

            // Act
            var result = sut.PlayerTurn(nextMoveIndex, expectedWinningChar);

            // Assert

            Assert.IsTrue(result.HasWinner);
            Assert.IsTrue(result.Winner.HasValue);
            Assert.IsTrue(result.WinningIndex == expectedWinningIndex);
            Assert.IsTrue(result.WinningDirection == expectedDirection);
            Assert.AreEqual(expectedWinningChar, result.Winner.Value);
        }
    }
}