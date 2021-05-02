using BattleShip.Controllers;
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

            var result = await _controller.CreateBoardAsync();
            Assert.IsInstanceOf<OkResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)((result as OkResult).StatusCode));
            _mockService.Verify(x => x.CreateBoardAsync(default), Times.Once);
        }

        [Test]
        public async Task Test_Add_BattleShip_Success()
        {
            var shipPos = new ShipPosition { Row = "A", Col = 1 };
            _mockService.MockAddBattleShip(shipPos);

            var result = (await _controller.AddBattleShipAsync(shipPos)).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)(result.StatusCode));
            Assert.AreEqual(true, result.Value);

            _mockService.Verify(x => x.AddBattleShipAsync(shipPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Add_BattleShip_Failed()
        {
            var shipPos = new ShipPosition { Row = "B", Col = 1 };
            _mockService.MockAddBattleShip(shipPos);

            var result = (await _controller.AddBattleShipAsync(shipPos)).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)(result.StatusCode));
            Assert.AreEqual(false, result.Value);

            _mockService.Verify(x => x.AddBattleShipAsync(shipPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Attack_Hit()
        {
            var markPos = new MarkPosition { Row = "A", Col = 1 };
            _mockService.MockAttack(markPos);

            var result = (await _controller.AttackAsync(markPos)).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsInstanceOf<AttackStatusEnum>(result.Value);

            Assert.AreEqual(AttackStatusEnum.Hit, result.Value);
            _mockService.Verify(x => x.AttackAsync(markPos, default), Times.Once);
        }

        [Test]
        public async Task Test_Attack_Miss()
        {
            var markPos = new MarkPosition { Row = "B", Col = 1 };
            _mockService.MockAttack(markPos);

            var result = (await _controller.AttackAsync(markPos)).Result as OkObjectResult;

            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(HttpStatusCode.OK, (HttpStatusCode)result.StatusCode);
            Assert.IsInstanceOf<AttackStatusEnum>(result.Value);

            Assert.AreEqual(AttackStatusEnum.Miss, result.Value);
            _mockService.Verify(x => x.AttackAsync(markPos, default), Times.Once);
        }
    }
}