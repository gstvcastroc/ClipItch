using System;
using ClipItch.API.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace ClipItch.API.Configuration
{
    public static class ConfigureDependencies
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {            
            var baseUrl = configuration["TwitchAPI"].ToString();

            services.AddRefitClient<IGameInterface>()
            .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

            services.AddRefitClient<IClipeInterface>()
            .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

            services.AddSingleton(new AutoMapperConfig());

            services.AddAutoMapper(typeof(Startup));

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