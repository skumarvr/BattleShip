using System.ComponentModel.DataAnnotations;

namespace BattleShip.ViewModels
{
    public class ShipPosition
    {
        public string Row { get; set; }

        public int Col { get; set; }

        public bool Vertical { get; set; }

        public int Length { get; set; }
    }
}
