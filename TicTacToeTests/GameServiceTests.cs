﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void Play_IfBoardSizeIsLessThan3_ShouldThrowException(int boardSize)
        {
            // Arrange
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(new GameState(boardSize));
            var gameService = GameService.CreateInstance(moqRenderer.Object);

            //Act/Assert
            Assert.ThrowsException<ArgumentException>(() => gameService.Play());
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(100)]
        public void Play_IfBoardSizeNotOddNumber_ShouldThrowException(int boardSize)
        {
            // Arrange
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(new GameState(boardSize));
            var gameService = GameService.CreateInstance(moqRenderer.Object);

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => gameService.Play());
        }

        [TestMethod]
        public void CreateInstance_WhenIRendererIsNull_ShouldThrowException()
        {
            // Arrange/Act/Assert
            Assert.ThrowsException<ArgumentException>(() => GameService.CreateInstance(null));
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(3)]
        [DataRow(5)]
        public void CreateInstance_IfRendererReturnsValidGameState_ShouldNotThrowException(int boardSize)
        {
            // Arrange/Act
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(new GameState());

            var o = GameService.CreateInstance(moqRenderer.Object);

            //Assert
            Assert.IsTrue(o is GameService);
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
        public void PlayerTurn_WhenWinningTurn_HasWinner_ShouldBeTrue(char[] board, int nextMoveIndex, int expectedWinningIndex, Direction expectedDirection, char expectedWinningChar)
        {
            // Arrange
            var moqGameState = new GameState((int)Math.Sqrt((double)board.Length));
            moqGameState.Board = board;
            
            var moqRenderer = new Mock<IRenderer>();
            moqRenderer.Setup(x => x.RenderStart()).Returns(moqGameState);

            var sut = GameService.CreateInstance(moqRenderer.Object);

            // Act
            var result = sut.PlayerTurn(moqGameState, nextMoveIndex, expectedWinningChar);

            // Assert

            Assert.IsTrue(result.TurnResult.HasWinner);
            Assert.IsTrue(result.TurnResult.Winner.HasValue);
            Assert.IsTrue(result.TurnResult.WinningIndex == expectedWinningIndex);
            Assert.IsTrue(result.TurnResult.WinningDirection == expectedDirection);
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

            var sut = GameService.CreateInstance(moqRenderer.Object);

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.PlayerTurn(moqGameState.Object, 1, 'X'));
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

            var sut = GameService.CreateInstance(moqRenderer.Object);

            // Act/Assert
            Assert.ThrowsException<ArgumentException>(() => sut.PlayerTurn(moqGameDetails.Object, nextMoveIndex, playerChar));
        }

        [TestMethod]
        [DataRow(new char[]
            {
                'X', 'X', 'O',
                'X','O','O',
                'X','O','X'
            }, 8)]
        public void PlayerTurn_WhenBoardIsComplete_ShouldThrowException(char[] board, int nextMoveIndex)
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

            var sut = GameService.CreateInstance(moqRenderer.Object);

            // Act/Assert
            Assert.ThrowsException<NotSupportedException>(() => sut.PlayerTurn(moqGameState.Object, 0, playerChar));

        }
    }
}