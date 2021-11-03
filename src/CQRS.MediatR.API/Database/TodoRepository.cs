using System.Collections.Generic;
using CQRS.MediatR.API.Database.Entities;

namespace CQRS.MediatR.API.Database
{
    public class TodoRepository
    {
        public List<Todo> Todos { get; } = new List<Todo>
        {
            new(1, "Cook Dinner", false),
            new(2, "Make Youtube video", true),
            new(3, "Wash car", false),
            new(4, "Programming 101", true),
            new(5, "Walk the Dawg", false)
        };
    }
}
