using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToeLibrary.Models;

namespace TicTacToeTests
{
    [TestClass]
    public class GameStateTests
    {
        [TestMethod]
        [DataRow(9, 3)]
        [DataRow(81, 9)]
        [DataRow(36, 6)]
        public void BoardSize_ShouldBeSquareRootOfBoardLength(int boardArraySize, int expectedResult)
        {
            // Arrange
            var sut = new GameState();
            sut.Board = new char[boardArraySize];

            // Act/Assert
            Assert.AreEqual(expectedResult, sut.BoardSize);
        }

        [TestMethod]
        [DataRow(new char[] { 'X', 'X', 'X' }, 3)]
        [DataRow(new char[] { 'X', 'O', 'X' }, 3)]
        [DataRow(new char[] { 'X', 'O', 'O' }, 3)]
        [DataRow(new char[] { 'O', 'O', 'O' }, 3)]
        public void Turns_ShouldEqualSumOfPlayerTurnsOnBoard(char[] board, int expectedResult)
        {
            // Arrange
            var sut = new GameState();
            sut.Board = board;
            sut.AllowedChars = board.Distinct().ToArray();

            // Act/Assert
            Assert.AreEqual(expectedResult, sut.Turns);
        }

        [TestMethod]
        [DataRow(new char[] { 'O', 'O', 'O' }, new char[] { 'O', 'X' }, 'X')]
        [DataRow(new char[] { 'X', 'O', 'X' }, new char[] { 'X', 'O' }, 'O')]
        [DataRow(new char[] { 'X', 'O', 'X' }, new char[] { 'O', 'X' }, 'O')]
        [DataRow(new char[] { 'X', 'X', 'X' }, new char[] { 'O', 'X' }, 'O')]
        public void CurrentPlayer_WhenTurnsIsOddNumber_ShouldEqualPlayerWithLeastMarksOnBoard(char[] board, char[] allowedChars, char expectedResult)
        {
            // Arrange
            var sut = new GameState()
            {
                AllowedChars = allowedChars,
                Board = board
            };

            // Act/Assert
            Assert.AreEqual(expectedResult, sut.CurrentPlayer);
        }

        [TestMethod]
        [DataRow(new char[] { 'X', 'O', 'X', 'O' }, new char[] { 'X', 'O' })]
        [DataRow(new char[] { 'X', 'O', 'X', 'O' }, new char[] { 'O', 'X' })]
        [DataRow(new char[] { 'O', 'X', 'O', 'X' }, new char[] { 'O', 'X' })]
        [DataRow(new char[] { 'O', 'X', 'O', 'X' }, new char[] { 'X', 'O' })]
        public void CurrentPlayer_WhenTurnsIEvenNumber_ShouldEqualFirstPlayerInAllowedChars(char[] board, char[] allowedChars)
        {
            // Arrange
            var sut = new GameState()
            {
                AllowedChars = allowedChars,
                Board = board
            };

            var expectedResult = allowedChars.First();

            // Act/Assert
            Assert.AreEqual(expectedResult, sut.CurrentPlayer);
        }
    }
}
