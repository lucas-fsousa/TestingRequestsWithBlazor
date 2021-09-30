using App.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Infrastructure.Contract;
using Server.Infrastructure.Data;

namespace Server.Infrastructure.Configuration {
  public static class ServiceConfiguration {
    /*it is necessary to reference the extension method "Extension ConfigureServices" in the startup of the application to be built.
     * 
     * The method below is an extension of IServiceCollection and allows to add useful information of the "startup" class so as not to overload it with too much information and apply the division of responsibilities correctly
     */
    public static IServiceCollection ExtensionConfigureServices(this IServiceCollection services, IConfiguration configuration) {

      // Captures the database connection string located in appsettings.json
      services.AddDbContext<Context>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x=> x.MigrationsAssembly(typeof(Context).Assembly.FullName).EnableRetryOnFailure()));

      // Definition of the generic repository for database manipulation
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

      return services;
    }
  }
}
