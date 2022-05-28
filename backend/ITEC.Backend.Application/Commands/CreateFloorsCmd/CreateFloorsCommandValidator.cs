using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorsCommandValidator : AbstractValidator<CreateFloorsCommand>
    {
        public CreateFloorsCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull()
                .WithMessage("Floor name cannot be null or empty!");

            RuleFor(c => c.Name).MaximumLength(250)
                .WithMessage("Floor name should have maximum of 250 characters!");

            RuleFor(c => c.OfficeId).NotNull()
                .WithMessage("Missing OfficeId!");
        }
    }
}
