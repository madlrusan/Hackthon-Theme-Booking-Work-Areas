using MediatR;

namespace ITEC.Backend.Application.Commands.SendEmailCmd
{
    public class SendEmailCommand : INotification
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
