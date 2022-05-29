using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Application.Commands.UserRegisterCmd
{
    public class UserRegisterCommandValidator : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterCommandValidator()
        {
            RuleFor(o => o.Email)
                .NotEmpty().NotNull()
                .WithMessage("Email is null or empty!");

            RuleFor(o => o.Email)
                .EmailAddress()
                .WithMessage("The provided email is not a valid email address!");

            RuleFor(o => o.Password)
                .NotEmpty().NotNull()
                .WithMessage("Password is null or empty!");
        }
    }
}
