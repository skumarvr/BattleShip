using BattleShip.Exceptions;
using BattleShip.Services;
using BattleShip.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.Tests
{
    public class GameServiceTests
    {
        private Mock<ILogger<GameService>> _mockLogger;
        private GameService _gameService;
        private string _gameId = "";

        [OneTimeSetUp]
        public async Task Init()
        {
            _mockLogger = new Mock<ILogger<GameService>>();
            _gameService = new GameService(_mockLogger.Object);

            _gameId = await _gameService.CreateBoardAsync();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {

        }


        [Test]
        public async Task Test_Add_BattleShip_Success()
        {
            var shipPos = new ShipPosition[] {
                new ShipPosition { Row = "A", Col = 1, Vertical = true, Length = 5 },
                new ShipPosition { Row = "A", Col = 1, Vertical = false, Length = 5 }
            };

            foreach (var pos in shipPos)
            {
                var result = await _gameService.AddBattleShipAsync(_gameId, pos);

                Assert.IsInstanceOf<bool>(result);
                Assert.AreEqual(true, result);
            }
        }

        [Test]
        public async Task Test_Add_BattleShip_Failed()
        {
            var shipPos = new ShipPosition[] {
                new ShipPosition { Row = "B", Col = 1, Vertical = false, Length = 5 },
                new ShipPosition { Row = "B", Col = 2, Vertical = true, Length = 5 },
            };

            foreach (var pos in shipPos)
            {
                var result = await _gameService.AddBattleShipAsync(_gameId, pos);

                Assert.IsInstanceOf<bool>(result);
                Assert.AreEqual(false, result);
            }
        }

        [Test]
        public async Task Test_Attack_Hit()
        {
            var markPos = new MarkPosition[] {
                new MarkPosition { Row = "B", Col = 2 },
                new MarkPosition { Row = "B", Col = 6 },
                new MarkPosition { Row = "D", Col = 3 },
            };

            foreach(var pos in markPos)
            {
                var result = await _gameService.AttackAsync(_gameId, pos);

                Assert.IsInstanceOf<AttackStatusEnum>(result);
                Assert.AreEqual(AttackStatusEnum.Hit, result);
            }
        }

        [Test]
        public async Task Test_Attack_Miss()
        {
            var markPos = new MarkPosition[] {
                new MarkPosition { Row = "A", Col = 5 },
                new MarkPosition { Row = "B", Col = 1 },
                new MarkPosition { Row = "B", Col = 7 },
            };

            foreach (var pos in markPos)
            {
                var result = await _gameService.AttackAsync(_gameId, pos);

                Assert.IsInstanceOf<AttackStatusEnum>(result);
                Assert.AreEqual(AttackStatusEnum.Miss, result);
            }
        }

        [Test]
        public void Test_Invalid_Game_Id()
        {   
            var gameId = "TestGame1";

            var markPos = new MarkPosition { Row = "A", Col = 5 };
            Assert.ThrowsAsync<InvalidGameIdException>(() => _gameService.AttackAsync(gameId, markPos));

            var shipPos = new ShipPosition { Row = "B", Col = 1 };
            Assert.ThrowsAsync<InvalidGameIdException>(() => _gameService.AddBattleShipAsync(gameId, shipPos));
        }
    }
}
        
