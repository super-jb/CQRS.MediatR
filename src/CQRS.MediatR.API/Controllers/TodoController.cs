using System.Threading.Tasks;
using CQRS.MediatR.API.CQRS.Commands;
using CQRS.MediatR.API.CQRS.Queries;
using CQRS.MediatR.API.Database.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.MediatR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult<Todo>> GetById(int id)
        {
            GetTodoByIdResponse response = await _mediator.Send(new GetTodoByIdQuery(id));

            return response == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AddTodoResponse>> Add([FromBody] AddTodoCommand command)
        {
            AddTodoResponse response = await _mediator.Send(new AddTodoCommand(command.Name));

            return Ok(response);
        }
    }
}
