using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ClipItch.API.Configuration
{
    public static class ConfigureDependencies
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
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