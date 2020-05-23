using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            Assert.ThrowsException<ArgumentException>(() => Game.CreateGame(boardSize));
        }

        [TestMethod]
        [DataRow(4)]
        [DataRow(6)]
        [DataRow(100)]
        public void Game_IfBoardSizeNotOddNumber_ShouldThrowException(int boardSize)
        {
            // Arrange/Act/Assert
            Assert.ThrowsException<ArgumentException>(() => Game.CreateGame(boardSize));
        }

        [TestMethod]
        [DataRow(99)]
        [DataRow(3)]
        [DataRow(5)]
        public void Game_IfBoardSizeIsValid_ShouldNotThrowException(int boardSize)
        {
            // Arrange/Act
            var o = Game.CreateGame(boardSize);

            //Assert
            Assert.IsTrue(o is Game);
        }
    }
}
