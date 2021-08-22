using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test
{
  public abstract class BaseTest
  {
    public void BaseText()
    {
    }
  }

  public class DbTeste : IDisposable
  {
    private string databaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
    public ServiceProvider ServiceProvider { get; private set; }

    public DbTeste()
    {
      var serviceColletion = new ServiceCollection();
      serviceColletion.AddDbContext<MyContext>(o =>
        o.UseMySql($"Persist Security Info=True; Server=localhost;Database={databaseName};User=root;Password=mudar@123"),
          ServiceLifetime.Transient
      );

      ServiceProvider = serviceColletion.BuildServiceProvider();
      using (var context = ServiceProvider.GetService<MyContext>())
      {
        context.Database.EnsureCreated();
      }
    }

    public void Dispose()
    {
      using (var context = ServiceProvider.GetService<MyContext>())
      {
        context.Database.EnsureDeleted();
      }
    }
  }

}
