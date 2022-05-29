using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.CreateReservationCmd
{
    public class CreateDeskReservationCommandValidator : AbstractValidator<CreateDeskReservationCommand>
    {
        public CreateDeskReservationCommandValidator()
        {
            RuleFor(x => x.DeskId).NotNull().NotEqual(0)
                .WithMessage("DeskId cannot be null or 0!");

            RuleFor(x => x.NumberOfDays).NotNull().GreaterThan(0)
                .WithMessage("NumberOfDays cannot be null or smaller than 1!");
        }
    }
}
