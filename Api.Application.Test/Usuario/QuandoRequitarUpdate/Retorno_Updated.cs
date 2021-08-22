using System;
using System.Threading.Tasks;
using Api.Application.Controllers;
using Api.Domain.Dtos.User;
using Api.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Api.Application.Test.Usuario.QuandoRequitarUpdate
{
  public class Retorno_Updated
  {
    private UsersController _controller;
    [Fact(DisplayName = "É possivel realizar o Updated")]
    public async Task E_Possivel_Invocar_Controller_Updated()
    {
      var serviceMock = new Mock<IUserService>();
      var nome = Faker.Name.FullName();
      var email = Faker.Internet.Email();

      serviceMock.Setup(m => m.Put(It.IsAny<UserDtoUpdate>())).ReturnsAsync(
        new UserDtoUpdateResult
        {
          Id = Guid.NewGuid(),
          Name = nome,
          Email = email,
          UpdateAt = DateTime.UtcNow
        }
      );
      _controller = new UsersController(serviceMock.Object);

      var userDtoUpdate = new UserDtoUpdate
      {
        Id = Guid.NewGuid(),
        Name = nome,
        Email = email
      };

      var result = await _controller.Put(userDtoUpdate);
      Assert.True(result is OkObjectResult);

      var resultValue = ((ObjectResult)result).Value as UserDtoUpdateResult;
      Assert.NotNull(resultValue);
      Assert.Equal(userDtoUpdate.Name, resultValue.Name);
      Assert.Equal(userDtoUpdate.Email, resultValue.Email);

    }
  }
}
