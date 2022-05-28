using ITEC.Backend.Application.Commands.CreateOfficeCmd;
using ITEC.Backend.Application.Queries.GetOfficeByIdQuery;
using ITEC.Backend.Application.Queries.GetOfficeQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITEC.Backend.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/office")]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOffices([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetOfficeByIdQuery() { OfficeId = id});
            return Ok(result);
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateOffice(CreateOfficeCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }
    }
}
