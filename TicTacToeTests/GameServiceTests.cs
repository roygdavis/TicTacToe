using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Enums;
using TicTacToeLibrary.Models;
using TicTacToeLibrary.Services;

namespace TicTacToeTests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        [DataRow(-100)]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(2)]
        public void CreateState_IfBoardSizeIsLessThan3_ShouldThrowException(int boardSize)
        {
            // Arrange
            var sut = new GameService();

            //Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CreateState(boardSize));
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(100)]
        public void CreateState_IfBoardSizeAnEvenNumber_ShouldThrowException(int boardSize)
        {
            // Arrange
            var sut = new GameService();

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CreateState(boardSize));
        }

        [TestMethod]
        [DataRow(new char[] { 'X', 'X' })]
        [DataRow(new char[] { 'X' })]
        [DataRow(new char[] { 'X', 'Y','Z' })]
        [DataRow(new char[] {  })]
        public void CreateState_IfPlayerCharsAreNotTwoDistinctValues_ShouldThrowException(char[] playerChars)
        {
            // Arrange
            var sut = new GameService();

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.CreateState(3, playerChars));
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(3)]
        [DataRow(5)]
        public void CreateState_IfValidBoardSize_ShouldNotThrowException(int boardSize)
        {
            // Arrange/Act
            var sut = new GameService();
            var gs = sut.CreateState(boardSize);

            //Assert
            Assert.IsTrue(sut is GameService);
            Assert.IsTrue(gs is GameState);
        }

        [TestMethod]
        [DataRow(new char[] { 'X', 'Y' })]
        [DataRow(new char[] { '1', '2' })]
        [DataRow(new char[] { '[', ']' })]
        public void CreateState_IfValidPlayerChars_ShouldNotThrowException(char[] playerChars)
        {
            // Arrange/Act
            var sut = new GameService();
            var gs = sut.CreateState(3, playerChars);

            //Assert
            Assert.IsTrue(sut is GameService);
            Assert.IsTrue(gs is GameState);
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
                'O', 'O', ' ',
                'X','X',' ',
                ' ',' ',' '
            }, 5, 1, Direction.Row, 'X')]
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
            }, 8, 0, Direction.TopLeftDiagonal, 'X')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'O','X','O',
                'X',' ',' '
            }, 8, 2, Direction.Column, 'O')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'O', 'X', 'X',
                'O', ' ', 'O'
            }, 7, 1, Direction.Column, 'X')]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ',' ','O',
                'X','O',' '
            }, 3, 0, Direction.Column, 'X')]
        [DataRow(new char[]
            {
                'X', 'O', 'X',
                'O', 'X', 'O',
                ' ', 'X', 'O'
            }, 6, 2, Direction.TopRightDiagonal, 'X')]
        public void PlayerTurn_WhenWinningTurn_HasWinner_ShouldBeTrue(char[] board, int nextMoveIndex, int expectedWinningIndex, Direction expectedDirection, char expectedWinningChar)
        {
            // Arrange
            var moqGameState = new GameState()
            {
                AllowedChars = new char[] { 'X', 'O' },
                Board = board
            };
            
            var sut = new GameService();

            // Act
            var result = sut.PlayerTurn(moqGameState, nextMoveIndex);

            // Assert
            Assert.IsTrue(result.TurnResult.HasWinner);
            Assert.IsTrue(result.TurnResult.Winner.HasValue);
            Assert.IsTrue(result.TurnResult.WinningDetails.Count == 1);
            Assert.IsTrue(result.TurnResult.WinningDetails.First().WinningIndex == expectedWinningIndex);
            Assert.IsTrue(result.TurnResult.WinningDetails.First().WinningDirection == expectedDirection);
            Assert.AreEqual(expectedWinningChar, result.TurnResult.Winner.Value);
        }

        [TestMethod]
        [DataRow(9)]
        [DataRow(19)]
        [DataRow(99)]
        public void PlayerTurn_WhenGameDetailsTurnsIsGreaterThanBoardSize_ShouldThrowException(int boardSize)
        {
            // Arrange
            var moqGameState = new Mock<IGameState>();
            moqGameState.Setup(x => x.BoardSize).Returns(boardSize);
            moqGameState.Setup(x => x.Turns).Returns(boardSize * boardSize);

            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(moqGameState.Object);

            var sut = new GameService();

            // Act/Assert
            Assert.ThrowsException<NotSupportedException>(() => sut.PlayerTurn(moqGameState.Object, 1));
        }

        [TestMethod]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 0)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 1)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 2)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'X','O','O',
                'X','O',' '
            }, 3)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 4)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 5)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 6)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O',' '
            }, 7)]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                ' ','O','O',
                'X','O','X'
            }, 8)]
        public void PlayerTurn_WhenPlayerMovesToPlaceAlreadyTaken_ShouldThrowException(char[] board, int nextMoveIndex)
        {
            // Arrange
            var playerChar = 'X';
            var moqGameDetails = new Mock<IGameState>();
            moqGameDetails.Setup(x => x.Board).Returns(board);
            moqGameDetails.Setup(x => x.BoardSize).Returns((int)Math.Sqrt((double)board.Length));
            moqGameDetails.Setup(x => x.Turns).Returns(board.Count(x => x == 'X' || x == 'O'));
            moqGameDetails.Setup(x => x.CurrentPlayer).Returns(playerChar);
            moqGameDetails.Setup(x => x.AllowedChars).Returns(new char[] { 'X', 'O' });

            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(moqGameDetails.Object);

            var sut = new GameService();

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.PlayerTurn(moqGameDetails.Object, nextMoveIndex));
        }

        [TestMethod]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'X','O','O',
                'X','O','X'
            }, 8)]
        public void PlayerTurn_WhenBoardIsComplete_AndPlayerTurnCalled_ShouldThrowException(char[] board, int nextMoveIndex)
        {
            // Arrange
            var playerChar = 'X';
            var moqGameState = new Mock<IGameState>();
            moqGameState.Setup(x => x.Board).Returns(board);
            moqGameState.Setup(x => x.BoardSize).Returns((int)Math.Sqrt((double)board.Length));
            moqGameState.Setup(x => x.Turns).Returns(board.Count(x => x == 'X' || x == 'O'));
            moqGameState.Setup(x => x.CurrentPlayer).Returns(playerChar);
            moqGameState.Setup(x => x.AllowedChars).Returns(new char[] { 'X', 'O' });

            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(moqGameState.Object);

            var sut = new GameService();

            // Act/Assert
            Assert.ThrowsException<NotSupportedException>(() => sut.PlayerTurn(moqGameState.Object, 0));
        }

        [TestMethod]
        [DataRow(new char[]
            {
                'X','O','X',
                'O','X','O',
                'O',' ','O'
            }, new char[] { 'X', 'O' }, 7, false)]
        public void PlayerTurn_WhenLastPlayerTurnIsCalled_AndThereIsNoWinner_ShouldReturnHasWinnerFalse(char[] board, char[] allowedChars, int nextMoveIndex, bool expectedResult)
        {
            // Arrange
            var playerChar = allowedChars.First();
            var moqGameState = new GameState();
            moqGameState.Board = board;
            moqGameState.AllowedChars = allowedChars;
            //moqGameState.BoardSize=).Returns((int)Math.Sqrt((double)board.Length));
            //moqGameState.Setup(x => x.Turns).Returns(board.Count(x => x == 'X' || x == 'O') + 1);
            //moqGameState.Setup(x => x.CurrentPlayer).Returns(playerChar);
            //moqGameState.Setup(x => x.AllowedChars).Returns(allowedChars);

            //var moqTurnResult = new Mock<ITurnResult>();

            //moqGameState.Setup(x => x.TurnResult).Returns(moqTurnResult.Object);

            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(moqGameState);

            var sut = new GameService();

            // Act
            var result = sut.PlayerTurn(moqGameState, nextMoveIndex);

            //Assert
            Assert.AreEqual(expectedResult, result.TurnResult.HasWinner);

        }
    }
}
