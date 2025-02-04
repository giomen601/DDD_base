using easypos.application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace easypos.application;
public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(config =>
    {
      config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
    });

    services.AddScoped(
      typeof(IPipelineBehavior<,>),
      typeof(ValidationBehavior<,>)
    );

    services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();

    return services;
  }
}