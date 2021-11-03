using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.API.Caching;
using CQRS.MediatR.API.Database;
using CQRS.MediatR.API.Database.Entities;
using CQRS.MediatR.API.DTOs;
using MediatR;

namespace CQRS.MediatR.API.CQRS.Queries
{
    public static class GetTodoById
    {
        // Query or Command: all the data needed to execute request
        public record Query(int Id) : IRequest<Response>, ICacheable
        {
            public string CacheKey => $"GetTodoById={Id}";
        }

        // Handler: all the business logic to execute -> returns a response
        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly TodoRepository _todoRepository;

            public Handler(TodoRepository todoRepository)
            {
                _todoRepository = todoRepository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                Todo? todo = _todoRepository.Todos.FirstOrDefault(x => x.Id == request.Id);

                return await Task.FromResult(todo == null ? null : new Response { Id = todo.Id, Name = todo.Name, Completed = todo.Completed });
            }
        }

        // Response: data we want to return
        public record Response : CqrsResponse
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public bool Completed { get; set; }
        }
    }
}
