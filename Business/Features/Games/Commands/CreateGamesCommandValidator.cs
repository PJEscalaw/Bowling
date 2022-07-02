using FluentValidation;

namespace Business.Features.Games.Commands
{
    public class CreateGamesCommandValidator : AbstractValidator<CreateGamesCommand>
    {
        public CreateGamesCommandValidator()
        {
            RuleFor(x => x.Dto.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
