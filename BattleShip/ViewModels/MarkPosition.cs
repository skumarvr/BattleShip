using System.ComponentModel.DataAnnotations;

namespace BattleShip.ViewModels
{
    public class MarkPosition
    {
        [Required]
        [RegularExpression(@"^[a-jA-J]{1}$", ErrorMessage = "Allow character from 'a-j' or 'A-J'")]
        public string Row { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Allow number from 1 to 10")]
        public int Col { get; set; }
    }
}
