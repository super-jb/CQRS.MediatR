using System.Threading.Tasks;

namespace CQRS.MediatR.API.Validations
{
    public interface IValidationHandler { }

    public interface IValidationHandler<T> : IValidationHandler
    {
        Task<ValidationResult> Validate(T request);
    }
}
