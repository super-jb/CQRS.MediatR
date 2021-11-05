using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.API.Database;
using CQRS.MediatR.API.Database.Entities;
using MediatR;

namespace CQRS.MediatR.API.CQRS.Queries
{
    public class GetTodoById : IRequestHandler<GetTodoByIdQuery, GetTodoByIdResponse>
    {
        private readonly TodoRepository _todoRepository;

        public GetTodoById(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<GetTodoByIdResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            Todo? todo = _todoRepository.Todos.FirstOrDefault(x => x.Id == request.Id);

            return await Task.FromResult(todo == null ? null : 
                new GetTodoByIdResponse
                {
                    Id = todo.Id,
                    Name = todo.Name,
                    Completed = todo.Completed
                });
        }
    }
}
