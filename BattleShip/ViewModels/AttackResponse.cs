using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

namespace BattleShip.ViewModels
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AttackStatusEnum
    {
        Hit,
        Miss,
    }

    public class AttackResponse
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AttackStatusEnum Status { get; set; }
    }
}
