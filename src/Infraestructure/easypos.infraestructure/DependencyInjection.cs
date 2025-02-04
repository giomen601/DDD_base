using easypos.infraestructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using easypos.application.Data;
using easypos.domain.Primitives;
using easypos.domain.Customers;
using easypos.infraestructure.Persistence.Repositories;

namespace easypos.infraestructure;
public static class DependencyInjection
{
  public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddPersistence(configuration);
    return services;
  }

  private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<ApplicationDbContext>
    (
      options => options.UseSqlServer(configuration.GetConnectionString("Database"))
    );

    services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
    services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());
    services.AddScoped<ICustomerRepository, CustomerRepository>();

    return services;
  }
}