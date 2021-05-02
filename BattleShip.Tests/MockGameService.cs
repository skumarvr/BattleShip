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
        public void MockAddBattleShip(ShipPosition shipPosition)
        {
            var addBattleship = Task.Run(() => {
                return (shipPosition.Row == "A" && shipPosition.Col == 1);
            });

            Setup(x => x.AddBattleShipAsync(shipPosition, default)).Returns(addBattleship);
        }

        public void MockAttack(MarkPosition markPosition)
        {
            // Return true only if the attack position is A1
            var attackTask = Task.Run(() => {
                return (markPosition.Row == "A" && markPosition.Col == 1)
                            ? AttackStatusEnum.Hit
                            : AttackStatusEnum.Miss;
            });

            Setup(x => x.AttackAsync(markPosition, default)).Returns(attackTask);
        }

        public void MockCreateBoard()
        {
            Setup(x => x.CreateBoardAsync(default)).Returns(Task.CompletedTask);
        }
    }
}
