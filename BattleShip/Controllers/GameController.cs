using BattleShip.ViewModels;
using BattleShip.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BattleShip.Exceptions;
using BattleShip.Validators;

namespace BattleShip.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GameController> _logger;
        private readonly IGameService _gameService;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _logger = logger;
            _gameService = gameService;
        }

        /// <summary>
        /// Welcome message
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public string Get()
        {
            return "Welcome to BattleShips game!!!";
        }

        /// <summary>
        /// Create board
        /// </summary>
        /// <returns>[GUID] Game id</returns>
        [HttpPost]
        [Route("CreateBoard")]
        public async Task<ActionResult<string>> CreateBoardAsync()
        {
            var gameId = await _gameService.CreateBoardAsync();
            return Ok(gameId);
        }

        /// <summary>
        /// Add battleship
        /// </summary>
        /// <param name="id">[GUID] game id obtained during create board</param>
        /// <param name="pos">[ShipPosition] Positon where the ship should be placed on the board</param>
        /// <returns>Status ( True or False )</returns>
        [HttpPost]
        [Route("AddBattleShip")]
        public async Task<ActionResult<AddBattleshipResponse>> AddBattleShipAsync([FromQuery]string id, [FromBody]ShipPosition pos)
        {
            var validator = new ShipPositionValidator();
            if (!validator.Validate(pos).IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _gameService.AddBattleShipAsync(id, pos);
                var response = new AddBattleshipResponse() { Status = result ? AddBattleshipStatusEnum.True : AddBattleshipStatusEnum.False };
                return Ok(response);
            } 
            catch (InvalidGameIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Check for Attack
        /// </summary>
        /// <param name="id">[GUID] game id obtained during create board</param>
        /// <param name="pos">[MarkPosition] Positon of the cell to be marked on the board</param>
        /// <returns>Status ("Hit" or "Miss"</returns>
        [HttpGet]
        [Route("Attack")]
        public async Task<ActionResult<AttackResponse>> AttackAsync([FromQuery]string id, [FromBody]MarkPosition pos)
        {
            var validator = new MarkPositionValidator();
            if (!validator.Validate(pos).IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _gameService.AttackAsync(id, pos);
                var response = new AttackResponse() { Status = result };
                return Ok(response);
            } 
            catch ( InvalidGameIdException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
