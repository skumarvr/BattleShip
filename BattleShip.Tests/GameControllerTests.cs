using BattleShip.Controllers;
using BattleShip.Exceptions;
using BattleShip.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace BattleShip.Tests
{
    public class GameControllerTests
    {
        private readonly Mock<ILogger<GameController>> _mockLogger;
        private readonly GameController _controller;
        private readonly MockGameService _mockService;

        public GameControllerTests()
        {
            _mockService = new MockGameService();
            _mockLogger = new Mock<ILogger<GameController>>();
            _controller = new GameController(_mockService.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Test_Create_Board()
        {
            _mockService.MockCreateBoard();

            var result = (await _controller.CreateBoardAsync()).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)(result.StatusCode));
            Assert.AreEqual("TestGame", result.Value);

            _mockService.Verify(x => x.CreateBoardAsync(default), Times.Once);
        }

        [Test]
        public async Task Test_Add_BattleShip_Success()
        {
            var shipPos = new ShipPosition { Row = "A", Col = 1 };
            var gameId = "TestGame";
            _mockService.MockAddBattleShip(gameId, shipPos);

            var result = (await _controller.AddBattleShipAsync(gameId, shipPos)).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)(result.StatusCode));
            Assert.IsInstanceOf<AddBattleshipResponse>(result.Value);

            var response = result.Value as AddBattleshipResponse;
            Assert.AreEqual(AddBattleshipStatusEnum.True, response.Status);

            _mockService.Verify(x => x.AddBattleShipAsync(gameId, shipPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Add_BattleShip_Failed()
        {
            var shipPos = new ShipPosition { Row = "B", Col = 1 };
            var gameId = "TestGame";
            _mockService.MockAddBattleShip(gameId, shipPos);

            var result = (await _controller.AddBattleShipAsync(gameId, shipPos)).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)(result.StatusCode));
            Assert.IsInstanceOf<AddBattleshipResponse>(result.Value);

            var response = result.Value as AddBattleshipResponse;
            Assert.AreEqual(AddBattleshipStatusEnum.False, response.Status);

            _mockService.Verify(x => x.AddBattleShipAsync(gameId, shipPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Attack_Hit()
        {
            var markPos = new MarkPosition { Row = "A", Col = 1 };
            var gameId = "TestGame";
            _mockService.MockAttack(gameId, markPos);

            var result = (await _controller.AttackAsync(gameId, markPos)).Result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsInstanceOf<AttackResponse>(result.Value);

            var response = result.Value as AttackResponse;
            Assert.AreEqual(AttackStatusEnum.Hit, response.Status);

            _mockService.Verify(x => x.AttackAsync(gameId, markPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Attack_Miss()
        {
            var markPos = new MarkPosition { Row = "B", Col = 1 };
            var gameId = "TestGame";
            _mockService.MockAttack(gameId, markPos);

            var result = (await _controller.AttackAsync(gameId, markPos)).Result as OkObjectResult;
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsInstanceOf<AttackResponse>(result.Value);

            var response = result.Value as AttackResponse;
            Assert.AreEqual(AttackStatusEnum.Miss, response.Status);

            _mockService.Verify(x => x.AttackAsync(gameId, markPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Invalid_Game_Id()
        {
            var gameId = "TestGame1";
            var ex = new InvalidGameIdException();

            var markPos = new MarkPosition { Row = "B", Col = 1 };
            _mockService.MockAttack(gameId, markPos);

            var result = (await _controller.AttackAsync(gameId, markPos)).Result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(ex.Message, result.Value);

            _mockService.Verify(x => x.AttackAsync(gameId, markPos, default), Times.Once);

            var shipPos = new ShipPosition { Row = "B", Col = 1 };
            _mockService.MockAddBattleShip(gameId, shipPos);

            result = (await _controller.AddBattleShipAsync(gameId, shipPos)).Result as BadRequestObjectResult;
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual(ex.Message, result.Value);

            _mockService.Verify(x => x.AddBattleShipAsync(gameId, shipPos, default), Times.Once);
        }
    }
}