using System;

namespace BattleShip.Exceptions
{
    public class InvalidGameIdException : Exception
    {
        public InvalidGameIdException() : base("Invalid Game Id") { }

        public InvalidGameIdException(string message) : base(message) { }
    }
}
