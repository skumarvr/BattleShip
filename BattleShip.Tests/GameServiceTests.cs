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
        private GameService gameService;

        [OneTimeSetUp]
        public async Task Init()
        {
            _mockLogger = new Mock<ILogger<GameService>>();
            gameService = new GameService(_mockLogger.Object);

            await gameService.CreateBoardAsync();
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
                var result = await gameService.AddBattleShipAsync(pos);

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
                var result = await gameService.AddBattleShipAsync(pos);

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
                var result = await gameService.AttackAsync(pos);

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
                var result = await gameService.AttackAsync(pos);

                Assert.IsInstanceOf<AttackStatusEnum>(result);
                Assert.AreEqual(AttackStatusEnum.Miss, result);
            }
        }
    }
}
        
