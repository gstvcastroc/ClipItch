using API.Data;
using API.Interfaces;
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
      // Adição do Refit.
      var baseUrl = configuration["TwitchAPI"].ToString();

      services.AddRefitClient<IGamesInterface>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      services.AddRefitClient<IClipsInterface>()
      .ConfigureHttpClient(options => options.BaseAddress = new Uri(baseUrl));

      // Adição do serviço de autenticação.
      services.AddSingleton<IAuthentication, Authentication>();

      // Adição do banco de dados.
      services.AddDbContext<DataContext>();
      services.AddSingleton<DataContext, DataContext>();

      // Adição dos serviços de jogos e clips.
      services.AddSingleton<IGamesService, GamesService>();
      services.AddSingleton<IClipsService, ClipsService>();
      services.AddSingleton<ISearchService, SearchService>();

      // Adição dos serviços de workers.
      services.AddScheduler();
      services.AddTransient<GameWorker>();
      services.AddTransient<ClipsWorker>();

      // Adição do swagger.
      services.AddSwaggerGen(c =>
      {
        // Informações da API.
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = "Clipitch",
          Version = "v1",
          Description = "Documentação dos endpoints da API do Clipitch",
          Contact = new OpenApiContact
          {
            Name = "Alonso de Oliveira, Gustavo Castro, Halex Maciel, Welbert Júnior",
            Url = new Uri("https://github.com/gstvcastroc/ClipItch")
          },
          License = new OpenApiLicense
          {
            Name = "License",
            Url = new Uri("https://openlearninglibrary.mit.edu/tos")
          }
        });

        // Configuração para buscar o XML necessário para a documentação via swagger.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

      });

      // Adição de versionamento da API.
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
      // Configuração dos workers.
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