using CQRS.MediatR.API.Caching;
using MediatR;

namespace CQRS.MediatR.API.CQRS.Queries
{
    public record GetTodoByIdQuery(int Id) : IRequest<GetTodoByIdResponse>, ICacheable
    {
        public string CacheKey => $"GetTodoById={Id}";
    }
}
