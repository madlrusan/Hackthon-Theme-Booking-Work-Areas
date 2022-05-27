using ITEC.Backend.Application.Commands.CreateOfficeCmd;
using ITEC.Backend.Application.Commands.GetOfficeQuery;
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
        public async Task<IActionResult> GetOffices([FromQuery] GetOfficeQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOffice(CreateOfficeCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(result);
        }
    }
}
