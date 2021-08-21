using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCuting.DependencyInjection
{
  public class ConfigureService
  {
    public static void ConfigureDependenciesService(IServiceCollection serviceColletion)
    {
      serviceColletion.AddTransient<IUserService, UserService>();
      serviceColletion.AddTransient<ILoginService, LoginService>();
    }
  }
}
