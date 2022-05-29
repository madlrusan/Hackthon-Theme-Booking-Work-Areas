using ITEC.Backend.Application.Commands.CreateReservationCmd;
using ITEC.Backend.Application.Queries.GetReservationQuery;
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

        [HttpGet]
        public async Task<IActionResult> GetReservation()
        {
            var result = await _mediator.Send(new GetReservationQuery());
            return Ok(result);
        }
    }
}
