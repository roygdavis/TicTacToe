using Moq;
using NUnit.Framework;
using TicTacToe.Services;

namespace TicTacToe.Tests
{
    public class GameServiceTests
    {
        private GameService _sut;

        [SetUp]
        public void Setup()
        {
            Mock<IRenderingService> _mockRenderer = new Mock<IRenderingService>();
            _sut = new GameService(_mockRenderer.Object);
        }

        [Test]
        public void WhenPlayerHasTurn_Should_DrawBoard()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}