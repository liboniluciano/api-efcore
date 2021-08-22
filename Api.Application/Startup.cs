using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Api.CrossCuting.DependencyInjection;
using Swashbuckle.AspNetCore;
using Api.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using Microsoft.AspNetCore.Authorization;
using Api.CrossCuting.Mappings;
using AutoMapper;

namespace application
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      _enviroment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment _enviroment { get; set; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      if (_enviroment.IsEnvironment("Testing"))
      {

        Environment.SetEnvironmentVariable("DB_CONNECTION", "Persist Security Info=True;Server=localhost;Port=3306;Database=dbAPI_Integration;Uid=root;Pwd=mudar@123");
        Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
        Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
        Environment.SetEnvironmentVariable("Audiente", "ExemploAudience");
        Environment.SetEnvironmentVariable("Issuer", "ExemploIssue");
        Environment.SetEnvironmentVariable("Seconds", "21600");
      }

      services.AddControllers();
      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependenciesRepository(services);

      var config = new AutoMapper.MapperConfiguration(cfg =>
      {
        cfg.AddProfile(new DtoToModelProfile());
        cfg.AddProfile(new EntityToDtoProfile());
        cfg.AddProfile(new ModelToEntityProfile());
      });

      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);

      var signingConfigurations = new SigningConfigurations();
      services.AddSingleton(signingConfigurations);

      var tokenConfigurations = new TokenConfigurations();
      new ConfigureFromConfigurationOptions<TokenConfigurations>(
        Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);
      services.AddSingleton(tokenConfigurations);

      services.AddAuthentication(authOptions =>
        {
          authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
          authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOptions =>
        {
          var paramsValidation = bearerOptions.TokenValidationParameters;
          paramsValidation.IssuerSigningKey = signingConfigurations.Key;
          paramsValidation.ValidAudience = tokenConfigurations.Audience;
          paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

          paramsValidation.ValidateIssuerSigningKey = true;
          paramsValidation.ValidateLifetime = true;

          paramsValidation.ClockSkew = TimeSpan.Zero;
        });

      services.AddAuthorization(auth =>
        {
          auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
              .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
              .RequireAuthenticatedUser().Build());
        });

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      // app.UseSwagger();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
