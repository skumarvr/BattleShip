using BattleShip.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace BattleShip.Services
{
    public interface IGameService
    {
        Task CreateBoardAsync(CancellationToken ct = default);
        Task AddBattleShipAsync(ShipPosition shipPosition, CancellationToken ct = default);
        Task<AttackStatusEnum> AttackAsync(MarkPosition markPosition, CancellationToken ct = default);
    }
}
