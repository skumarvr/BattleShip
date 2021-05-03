using BattleShip.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.Services
{
    public interface IGameService
    {
        Task<string> CreateBoardAsync(CancellationToken ct = default);
        Task<bool> AddBattleShipAsync(string gameId, ShipPosition shipPosition, CancellationToken ct = default);
        Task<AttackStatusEnum> AttackAsync(string gameId, MarkPosition markPosition, CancellationToken ct = default);
    }
}
