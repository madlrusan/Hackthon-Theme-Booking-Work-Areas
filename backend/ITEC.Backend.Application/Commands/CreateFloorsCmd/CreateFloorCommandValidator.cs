using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateFloorsCmd
{
    public class CreateFloorCommandValidator : AbstractValidator<CreateFloorCommand>
    {
        public CreateFloorCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull()
                .WithMessage("Floor name cannot be null or empty!");

            RuleFor(c => c.Columns).NotNull().GreaterThan(0)
                .WithMessage("The number of columns cannot be null or smaller than 0!");

            RuleFor(c => c.Rows).NotNull().GreaterThan(0)
               .WithMessage("The number of columns cannot be null or smaller than 0!");

            RuleFor(c => c.Name).NotEmpty().NotNull()
                .WithMessage("Floor name cannot be null or empty!");

            RuleFor(c => c.Name).MaximumLength(250)
                .WithMessage("Floor name should have maximum of 250 characters!");

            RuleFor(c => c.Desks).NotEmpty().NotNull()
                .WithMessage("Desks list cannot be null or empty!");

            RuleForEach(c => c.Desks).SetValidator(new CreateDeskCommandValidator());
        }
    }
}
