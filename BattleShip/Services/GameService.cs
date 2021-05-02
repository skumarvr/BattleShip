using BattleShip.ViewModels;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.Services
{
    public class GameService:IGameService
    {
        private const int boardSize = 10;
        private char[][] myShips;

        public GameService(ILogger<GameService> logger)
        {
            myShips = new char[boardSize][];
            for (int row = 0; row < boardSize; row++)
            {
                myShips[row] = new char[boardSize];
            }
        }

        public async Task CreateBoardAsync(CancellationToken ct = default)
        {
            await InitialiseBoardUsingSampleData();
        }

        public async Task<bool> AddBattleShipAsync(ShipPosition shipPosition, CancellationToken ct = default)
        {
            int row = shipPosition.Row.ToUpper()[0] - 65;
            int col = shipPosition.Col - 1;

            return await Task.Run(() => CheckBattleShipCanBeAdded(shipPosition));
        }

        public async Task<AttackStatusEnum> AttackAsync(MarkPosition markPosition, CancellationToken ct = default)
        {
            return await Task.Run(() => CheckAttack(markPosition));
        }

        private bool CheckBattleShipCanBeAdded(ShipPosition shipPosition)
        {
            int row = shipPosition.Row.ToUpper()[0] - 65;
            int col = shipPosition.Col - 1;
            if(shipPosition.Vertical)
            {

            }
            else
            {

            }

            bool shipExists = false;
            for(int i=0;i<shipPosition.Length; i++)
            {
                if(shipPosition.Vertical)
                {
                    shipExists = (myShips[row+i][col] == 'S');
                }
                else
                {
                    shipExists = (myShips[row][col+i] == 'S');
                }
                if (shipExists) break;

            }
            return !shipExists;
        }

        private AttackStatusEnum CheckAttack(MarkPosition markPosition)
        {
            int row = markPosition.Row.ToUpper()[0] - 65;
            int col = markPosition.Col - 1;
            return (myShips[row][col] == 'S')
                        ? AttackStatusEnum.Hit
                        : AttackStatusEnum.Miss;
        }

        private async Task InitialiseBoardUsingSampleData()
        {
            var filePath = @".\SampleBoard\myShips.csv";
            var data = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    myShips[row][col] = data[row][col][0];
                }
            }

            await Task.CompletedTask;
        }

    }
}
