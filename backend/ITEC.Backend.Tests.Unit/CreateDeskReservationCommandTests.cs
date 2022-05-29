using ITEC.Backend.Application.Commands.CreateReservationCmd;
using ITEC.Backend.Application.Shared;
using ITEC.Backend.Domain.Models;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ITEC.Backend.Tests.Unit
{
    public class CreateDeskReservationCommandTests
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IDeskReservationRepository> _deskReservationRepositoryMock;
        private Mock<IRepository<Desk>> _deskRepositoryMock;
        private Mock<IMediator> _mediatorMock;

        public CreateDeskReservationCommandTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _deskReservationRepositoryMock = new Mock<IDeskReservationRepository>();
            _deskRepositoryMock = new Mock<IRepository<Desk>>();
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task ReservationCreationFails_WhenDeskIdIsMissing()
        {
            var cmd = new CreateDeskReservationCommand() { NumberOfDays = 5};

            var handler = GetHandler();

            var exception = await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(cmd, CancellationToken.None));

            Assert.Equal("DeskId cannot be null or 0!", exception.ValidationErrors[0]);
        }

        private CreateDeskReservationCommandHandler GetHandler()
        {
            return new CreateDeskReservationCommandHandler(
                _httpContextAccessorMock.Object,
                _deskReservationRepositoryMock.Object,
                _deskRepositoryMock.Object,
                _mediatorMock.Object);
        }
    }
}
