using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vimo.ApplicationCore.Validations;

namespace Vimo.Infrastructure.Validations
{
    public static class ValidationsEngineExtensions
    {
        public static IServiceCollection RegisterValidations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(SaveUserCommandValidator).Assembly);


            return services;
        }
    }
}