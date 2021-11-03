using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.MediatR.API.Validations
{
    public static class ValidationExtensions
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IValidationHandler>()
                .AddClasses(classes => classes.AssignableTo<IValidationHandler>())
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }
    }
}
