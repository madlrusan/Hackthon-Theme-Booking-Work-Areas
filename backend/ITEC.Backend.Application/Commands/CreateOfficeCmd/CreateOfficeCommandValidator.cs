using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateOfficeCmd
{
    public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
    {
        public CreateOfficeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull()
                .WithMessage("Office name cannot be null or empty!");

            RuleFor(x => x.Name)
                .MaximumLength(250)
                .WithMessage("Office name cannot be longer than 250 characters!");
        }
    }
}
