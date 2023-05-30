using API.Data;
using API.Interfaces;
using API.Interfaces.API;
using API.Services;
using API.Workers;
using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Refit;
using System;
using System.IO;
using System.Reflection;

namespace API.Configuration
{
  public static class ConfigureDependencies
  {
    public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
    {
      // Adi��o do Refit.
      var baseUrl = configuration["TwitchAPI"].ToString();

      services.AddRefitClient<IGamesContract>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      services.AddRefitClient<IClipsContract>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      services.AddRefitClient<IUsersContract>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      // Adi��o do servi�o de autentica��o.
      services.AddSingleton<IAuthenticationContract, Authentication>();

      // Adi��o do banco de dados.
      services.AddDbContext<DataContext>();
      services.AddSingleton<DataContext, DataContext>();

      // Adi��o dos servi�os de jogos e clips.
      services.AddSingleton<IGamesService, GamesService>();
      services.AddSingleton<IClipsService, ClipsService>();
      services.AddSingleton<ISearchService, SearchService>();
      services.AddSingleton<IUsersService, UsersService>();

      // Adi��o dos servi�os de workers.
      services.AddScheduler();
      services.AddTransient<GameWorker>();
      services.AddTransient<ClipsWorker>();

      // Adi��o do swagger.
      services.AddSwaggerGen(c =>
      {
        // Informa��es da API.
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Clipitch",
          Version = "v1",
          Description = "Documenta��o dos endpoints da API do Clipitch",
          Contact = new OpenApiContact
          {
            Name = "Alonso de Oliveira, Gustavo Castro, Halex Maciel, Welbert J�nior",
            Url = new Uri("https://github.com/gstvcastroc/ClipItch")
          },
          License = new OpenApiLicense
          {
            Name = "License",
            Url = new Uri("https://openlearninglibrary.mit.edu/tos")
          }
        });

        // Configura��o para buscar o XML necess�rio para a documenta��o via swagger.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

      });

      // Adi��o de versionamento da API.
      services.AddApiVersioning();
      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
      });

      return services;
    }

    public static IApplicationBuilder ResolveBuild(this IApplicationBuilder app)
    {
      // Configura��o dos workers.
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

      return app;
    }
  }
}