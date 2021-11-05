using CQRS.MediatR.API.DTOs;

namespace CQRS.MediatR.API.CQRS.Queries
{
    public record GetTodoByIdResponse : CqrsResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Completed { get; set; }
    }
}
