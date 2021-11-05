using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.API.Database;
using CQRS.MediatR.API.Database.Entities;
using MediatR;

namespace CQRS.MediatR.API.CQRS.Commands
{
    public class AddTodo : IRequestHandler<AddTodoCommand, AddTodoResponse>
    {
        private readonly TodoRepository _todoRepository;

        public AddTodo(TodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<AddTodoResponse> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            int maxId = _todoRepository.Todos.Select(x => x.Id).Max();
            _todoRepository.Todos.Add(new Todo(++maxId, request.Name, false));

            return await Task.FromResult(new AddTodoResponse { Id = maxId });
        }
}
}
