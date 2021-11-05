using MediatR;

namespace CQRS.MediatR.API.CQRS.Commands
{
    public record AddTodoCommand(string Name) : IRequest<AddTodoResponse>;
}
