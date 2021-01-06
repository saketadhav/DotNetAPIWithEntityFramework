using FluentValidation;

namespace Application.Funds.Commands.CreateFund
{
    public class CreateFundCommandValidator : AbstractValidator<CreateFundCommand>
    {
        public CreateFundCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
