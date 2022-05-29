using ITEC.Backend.Application.Commands.CreateFloorsCmd;
using ITEC.Backend.Application.Queries.GetFloorByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITEC.Backend.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/floor")]
    public class FloorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FloorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateFloor(CreateFloorsCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFloor([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetFloorByIdQuery() { FloorId = id });
            return Ok(result);
        }
    }
}
