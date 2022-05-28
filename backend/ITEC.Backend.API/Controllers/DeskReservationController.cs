using ITEC.Backend.Application.Commands.CreateReservationCmd;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITEC.Backend.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/reservation")]
    public class DeskReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeskReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeskReservation(CreateDeskReservationCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }
    }
}
