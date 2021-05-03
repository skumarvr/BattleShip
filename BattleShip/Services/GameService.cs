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

        /// <summary>
        /// Create board
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<string> CreateBoardAsync(CancellationToken ct = default)
        {
            _gameId = await Task.Run(() => InitialiseBoardUsingSampleData());
            return _gameId;
        }

        /// <summary>
        /// Add Battleship
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="shipPosition"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<bool> AddBattleShipAsync(string gameId, ShipPosition shipPosition, CancellationToken ct = default)
        {
            if (gameId != _gameId) throw new InvalidGameIdException();

            int row = shipPosition.Row.ToUpper()[0] - 65;
            int col = shipPosition.Col - 1;

            return await Task.Run(() => CheckBattleShipCanBeAdded(shipPosition));
        }

        /// <summary>
        /// Attack
        /// </summary>
        /// <param name="gameId"></param>
        /// <param name="markPosition"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<AttackStatusEnum> AttackAsync(string gameId, MarkPosition markPosition, CancellationToken ct = default)
        {
            if (gameId != _gameId) throw new InvalidGameIdException();

            return await Task.Run(() => CheckAttack(markPosition));
        }

        /// <summary>
        /// Function to check if the battleship can be added to the board
        /// </summary>
        /// <param name="shipPosition"></param>
        /// <returns></returns>
        private bool CheckBattleShipCanBeAdded(ShipPosition shipPosition)
        {
            int row = shipPosition.Row.ToUpper()[0] - 65;
            int col = shipPosition.Col - 1;

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


        /// <summary>
        /// Function to check if attach is a hit or miss
        /// ( Using myShip board instead of the opponent ship board. )
        /// </summary>
        /// <param name="markPosition"></param>
        /// <returns></returns>
        private AttackStatusEnum CheckAttack(MarkPosition markPosition)
        {
            int row = markPosition.Row.ToUpper()[0] - 65;
            int col = markPosition.Col - 1;
            return (_myShips[row][col] == 'S')
                        ? AttackStatusEnum.Hit
                        : AttackStatusEnum.Miss;
        }

        /// <summary>
        /// Initialising my ship board data from sample csv file.
        /// </summary>
        /// <returns></returns>
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
