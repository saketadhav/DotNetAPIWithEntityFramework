using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Funds.Commands.UpdateFund
{
    public class UpdateFundCommandValidator : AbstractValidator<UpdateFundCommand>
    {
        public UpdateFundCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
