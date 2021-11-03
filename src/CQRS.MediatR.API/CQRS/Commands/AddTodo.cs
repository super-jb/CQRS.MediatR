using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.API.Database;
using CQRS.MediatR.API.Database.Entities;
using CQRS.MediatR.API.DTOs;
using CQRS.MediatR.API.Validations;
using MediatR;

namespace CQRS.MediatR.API.CQRS.Commands
{
    public class AddTodo
    {
        // Command
        public record Command(string Name) : IRequest<Response>;

        // Validator
        public class Validator : IValidationHandler<Command>
        {
            private readonly TodoRepository _todoRepository;

            public Validator(TodoRepository todoRepository)
            {
                _todoRepository = todoRepository;
            }

            public async Task<ValidationResult> Validate(Command request)
            {
                ValidationResult result = ValidationResult.Success;
                if (_todoRepository.Todos.Any(x => x.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    result = ValidationResult.Fail("Item already exists");
                }

                return await Task.FromResult(result);
            }
        }

        /* TODO: See if you can implement FluentValidations
        public class AddTodoValidator : AbstractValidator<Command>
        {
            private readonly TodoRepository _todoRepository;

            public AddTodoValidator(TodoRepository todoRepository)
            {
                _todoRepository = todoRepository;

                RuleFor(x => x.Name).NotEmpty();

                RuleFor(x => x.Name)
                    .Must((entity, value, c) => IsNameUnique(value))
                    .WithMessage("Item already exists");
            }

            private bool IsNameUnique(string name)
            {
                if (_todoRepository.Todos.Any(t => t.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }

                return true;
            }
        }        
        */

        // Handler
        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly TodoRepository _todoRepository;

            public Handler(TodoRepository todoRepository)
            {
                _todoRepository = todoRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                int maxId = _todoRepository.Todos.Select(x => x.Id).Max();
                _todoRepository.Todos.Add(new Todo(++maxId, request.Name, false));

                return await Task.FromResult(new Response { Id = maxId });
            }
        }

        // Response: data we want to return
        public record Response : CqrsResponse
        {
            public int Id { get; set; }
        }
    }
}
