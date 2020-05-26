using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using TicTacToe.Classes;
using Moq;

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
            // Arrange
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(new GameDetails(boardSize));

            //Act/Assert
            Assert.ThrowsException<ArgumentException>(() => Game.CreateInstance(moqRenderer.Object));
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(100)]
        public void Game_IfBoardSizeNotOddNumber_ShouldThrowException(int boardSize)
        {
            // Arrange
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(new GameDetails(boardSize));

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => Game.CreateInstance(moqRenderer.Object));
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(3)]
        [DataRow(5)]
        public void Game_IfBoardSizeIsValid_ShouldNotThrowException(int boardSize)
        {
            // Arrange/Act
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(new GameDetails());

            var o = Game.CreateInstance(moqRenderer.Object);

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
            var moqGameDetails = new GameDetails();
            moqGameDetails.Board = board;
            moqGameDetails.BoardSize = (int)Math.Sqrt((double)board.Length);
            moqGameDetails.Turns = board.Count(x => x == 'X' || x == 'O');

            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(moqGameDetails);

            var sut = Game.CreateInstance(moqRenderer.Object);

            // Act
            var result = sut.PlayerTurn(nextMoveIndex, expectedWinningChar);

            // Assert

            Assert.IsTrue(result.HasWinner);
            Assert.IsTrue(result.Winner.HasValue);
            Assert.IsTrue(result.WinningIndex == expectedWinningIndex);
            Assert.IsTrue(result.WinningDirection == expectedDirection);
            Assert.AreEqual(expectedWinningChar, result.Winner.Value);
        }

        [TestMethod]
        [DataRow(9)]
        [DataRow(19)]
        [DataRow(99)]
        public void Game_WhenGameDetailsTurnsIsGreaterThanBoardSize_ShouldThrowException(int boardSize)
        {
            // Arrange
            var moqGameDetails = new Mock<IGameDetails>();
            moqGameDetails.Setup(x => x.BoardSize).Returns(boardSize);
            moqGameDetails.Setup(x => x.Turns).Returns(boardSize * boardSize);
            var sut = new Game() { GameDetails = moqGameDetails.Object };

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.PlayerTurn(1, 'X'));
        }
    }
}