using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BattleShip.ViewModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AddBattleshipStatusEnum
    {
        True,
        False,
    }

    /// <summary>
    /// AddBattleship Response
    /// </summary>
    public class AddBattleshipResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AddBattleshipStatusEnum Status { get; set; }
    }
}
