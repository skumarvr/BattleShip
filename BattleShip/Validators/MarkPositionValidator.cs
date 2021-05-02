using BattleShip.ViewModels;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace BattleShip.Validators
{
    public class MarkPositionValidator : AbstractValidator<MarkPosition>
    {
        public MarkPositionValidator()
        {
            // MarkPosition.Row
            RuleFor(p => p.Row).NotEmpty();
            RuleFor(p => p.Row).NotNull();
            RuleFor(p => p.Row).Matches(@"^[a-jA-J]{1}$")
                .WithMessage("Please enter character from 'a-j' or 'A-J'");

            // MarkPosition.Col
            RuleFor(p => p.Col).GreaterThanOrEqualTo(1)
                .WithMessage("Please enter number from 1 to 10");
            RuleFor(p => p.Col).LessThanOrEqualTo(10)
                .WithMessage("Please enter number from 1 to 10");
        }
    }
}
