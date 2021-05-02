using BattleShip.ViewModels;
using BattleShip.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        [Route("CreateBoard")]
        public async Task<IActionResult> CreateBoardAsync()
        {
            await _gameService.CreateBoardAsync();
            return Ok();
        }

        [HttpPost]
        [Route("AddBattleShip")]
        public async Task<ActionResult<bool>> AddBattleShipAsync(ShipPosition pos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addedBattleShip = await _gameService.AddBattleShipAsync(pos);

            return Ok(addedBattleShip);
        }

        [HttpGet]
        [Route("Attack")]
        public async Task<ActionResult<AttackStatusEnum>> AttackAsync(MarkPosition pos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attackStatus = await _gameService.AttackAsync(pos);
            return Ok(attackStatus);
        }
    }
}
