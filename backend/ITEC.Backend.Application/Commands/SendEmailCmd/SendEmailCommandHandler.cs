using ITEC.Backend.Application.Options;
using ITEC.Backend.Application.Shared;
using MediatR;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ITEC.Backend.Application.Commands.SendEmailCmd
{
    public class SendEmailCommandHandler : INotificationHandler<SendEmailCommand>
    {
        private readonly SendgridOptions _sendgridOptions;

        public SendEmailCommandHandler(IOptions<SendgridOptions> sendgridOptions)
        {
            _sendgridOptions = sendgridOptions.Value;
        }

        public async Task Handle(SendEmailCommand notification, CancellationToken cancellationToken)
        {
            var validator = new SendEmailCommandValidator();
            var validatorResult = await validator.ValidateAsync(notification);
            if (validatorResult.Errors.Count > 0)
            {
                throw new ValidationException(validatorResult);
            }

            var senderAddress = new EmailAddress()
            { Name = _sendgridOptions.SenderName, Email = _sendgridOptions.SenderEmail };
            var message = new SendGridMessage
            {
                From = senderAddress,
                Subject = notification.Subject,
                PlainTextContent = notification.Body,
                HtmlContent = notification.Body
            };

            // skip sending emails to real boxes for dev purposes and to prevent spam
            if (_sendgridOptions.DevMode.HasValue && _sendgridOptions.DevMode.Value)
                message.AddTo(new EmailAddress(_sendgridOptions.DevModeReceiver));
            else
            {
                message.AddTo(new EmailAddress(notification.Receiver));
            }

            var client = new SendGridClient(_sendgridOptions.ApiKey);
            var result = await client.SendEmailAsync(message);
        }
    }
}
