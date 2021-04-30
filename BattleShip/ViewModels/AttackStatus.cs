using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleShip.ViewModels
{
    public enum AttackStatusEnum
    {
        Hit,
        Miss,
    }

    public class AttackStatus
    {
        public AttackStatusEnum Status { get; set; }
    }
}
