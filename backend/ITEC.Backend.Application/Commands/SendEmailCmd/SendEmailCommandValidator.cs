using FluentValidation;

namespace ITEC.Backend.Application.Commands.SendEmailCmd
{
    public class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(o => o.Receiver)
                .NotEmpty().NotNull()
                .WithMessage("Email is null or empty!");

            RuleFor(o => o.Subject)
                .NotNull().NotEmpty()
                .WithMessage("Email subject is null or empty!");

            RuleFor(o => o.Body)
                .NotNull().NotEmpty()
                .WithMessage("Email body is null or empty!");
        }
    }
}
