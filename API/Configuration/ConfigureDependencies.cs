using API.Data;
using API.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace API.Configuration
{
  public static class ConfigureDependencies
  {
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
      // Adição do Refit
      var baseUrl = configuration["TwitchAPI"].ToString();

      services.AddRefitClient<IGamesInterface>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      services.AddRefitClient<IClipsInterface>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      // Adição do serviço de banco de dados
      services.AddDbContext<DataContext>();
      services.AddSingleton<DataContext, DataContext>();

      return services;
    }

    public static IApplicationBuilder ResolveBuild(this IApplicationBuilder app)
    {
      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
              name: "default",
              pattern: "{controller=Home}/{action=Index}/{id?}");
      });

      return app;
    }
  }
}