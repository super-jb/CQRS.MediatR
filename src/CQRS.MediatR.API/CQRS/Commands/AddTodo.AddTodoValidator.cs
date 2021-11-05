using System;
using System.Linq;
using CQRS.MediatR.API.Database;
using FluentValidation;

namespace CQRS.MediatR.API.CQRS.Commands
{
    public class AddTodoValidator : AbstractValidator<AddTodoCommand>
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
}
