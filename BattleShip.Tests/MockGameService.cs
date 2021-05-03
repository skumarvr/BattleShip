using BattleShip.Exceptions;
using BattleShip.Services;
using BattleShip.ViewModels;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.Tests
{
    public class MockGameService : Mock<IGameService>
    {
        public void MockAddBattleShip(string gameId, ShipPosition shipPosition)
        {
            var addBattleship = Task.Run(() => {
                if (gameId != "TestGame") throw new InvalidGameIdException();
                return (gameId == "TestGame" && shipPosition.Row == "A" && shipPosition.Col == 1);
            });

            Setup(x => x.AddBattleShipAsync(gameId, shipPosition, default)).Returns(addBattleship);
        }

        public void MockAttack(string gameId, MarkPosition markPosition)
        {
            // Return true only if the attack position is A1
            var attackTask = Task.Run(() => {
                if (gameId != "TestGame") throw new InvalidGameIdException();
                return (gameId == "TestGame" && markPosition.Row == "A" && markPosition.Col == 1)
                            ? AttackStatusEnum.Hit
                            : AttackStatusEnum.Miss;
            });

            Setup(x => x.AttackAsync(gameId, markPosition, default)).Returns(attackTask);
        }

        public void MockCreateBoard()
        {
            // Return true only if the attack position is A1
            var CreateBoard = Task.Run(() => {
                return "TestGame";
            });

            Setup(x => x.CreateBoardAsync(default)).Returns(CreateBoard);
        }
    }
}
