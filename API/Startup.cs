using System;
using API.Configuration;
using API.Services;
using API.Workers;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddSingleton<IGamesService, GamesService>();
      services.AddSingleton<IClipsService, ClipsService>();
      services.AddSingleton<IAuthentication, Authentication>();

      services.AddScheduler();
      services.AddTransient<GameWorker>();
      services.AddTransient<ClipsWorker>();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "ClipItch",
          Version = "v1",
          Description = "Documentação dos EndPoints do Clipitch API",
          Contact = new OpenApiContact
          {
            Name = "Gustavo Castro",
            Url = new Uri("https://github.com/gstvcastroc")
          },
          License = new OpenApiLicense
          {
            Name = "License",
            Url = new Uri("https://openlearninglibrary.mit.edu/tos")
          }
        });
      });

      services.ResolveDependencies(Configuration);
    }
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      var provider = app.ApplicationServices;
      provider.UseScheduler(scheduler =>
      {
        scheduler
        .Schedule<GameWorker>()
        .Daily();
      });

      provider.UseScheduler(scheduler =>
      {
        scheduler
        .Schedule<ClipsWorker>()
        .Daily();
      });

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClipItch"); c.RoutePrefix = string.Empty; });
      }

      app.ResolveBuild();
    }
  }
}
