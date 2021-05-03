using BattleShip.Exceptions;
using BattleShip.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.Services
{
    public class GameService:IGameService
    {
        private const int _boardSize = 10;
        private char[][] _myShips;
        private string _gameId = "";

         public GameService(ILogger<GameService> logger)
        {
            _myShips = new char[_boardSize][];
            for (int row = 0; row < _boardSize; row++)
            {
                _myShips[row] = new char[_boardSize];
            }
        }

        public async Task<string> CreateBoardAsync(CancellationToken ct = default)
        {
            _gameId = await Task.Run(() => InitialiseBoardUsingSampleData());
            return _gameId;
        }

        public async Task<bool> AddBattleShipAsync(string gameId, ShipPosition shipPosition, CancellationToken ct = default)
        {
            if (gameId != _gameId) throw new InvalidGameIdException();

            int row = shipPosition.Row.ToUpper()[0] - 65;
            int col = shipPosition.Col - 1;

            return await Task.Run(() => CheckBattleShipCanBeAdded(shipPosition));
        }

        public async Task<AttackStatusEnum> AttackAsync(string gameId, MarkPosition markPosition, CancellationToken ct = default)
        {
            if (gameId != _gameId) throw new InvalidGameIdException();

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
                    shipExists = (_myShips[row+i][col] == 'S');
                }
                else
                {
                    shipExists = (_myShips[row][col+i] == 'S');
                }
                if (shipExists) break;

            }
            return !shipExists;
        }

        private AttackStatusEnum CheckAttack(MarkPosition markPosition)
        {
            int row = markPosition.Row.ToUpper()[0] - 65;
            int col = markPosition.Col - 1;
            return (_myShips[row][col] == 'S')
                        ? AttackStatusEnum.Hit
                        : AttackStatusEnum.Miss;
        }

        private string InitialiseBoardUsingSampleData()
        {
            var filePath = @".\SampleBoard\myShips.csv";
            var data = File.ReadLines(filePath).Select(x => x.Split(',')).ToArray();
            for (int row = 0; row < _boardSize; row++)
            {
                for (int col = 0; col < _boardSize; col++)
                {
                    _myShips[row][col] = data[row][col][0];
                }
            }

            return Guid.NewGuid().ToString();
        }
    }
}
