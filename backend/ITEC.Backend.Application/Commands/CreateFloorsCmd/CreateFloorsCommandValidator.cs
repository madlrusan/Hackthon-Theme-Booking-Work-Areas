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
            RuleFor(c => c.OfficeId).NotNull()
                .WithMessage("Missing OfficeId!");

            RuleFor(c => c.Floors).NotEmpty().NotNull()
                .WithMessage("Floors list cannot be null or empty!");

            RuleForEach(c => c.Floors).SetValidator(new CreateFloorCommandValidator());
        }
    }
}
