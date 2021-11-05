using CQRS.MediatR.API.DTOs;

namespace CQRS.MediatR.API.CQRS.Commands
{
    public record AddTodoResponse : CqrsResponse
    {
        public int Id { get; set; }
    }
}
