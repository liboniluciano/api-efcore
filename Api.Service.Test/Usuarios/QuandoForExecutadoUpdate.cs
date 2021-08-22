using System.Threading.Tasks;
using Api.Domain.Interfaces.Services.User;
using Moq;
using Xunit;

namespace Api.Service.Test.Usuarios
{
  public class QuandoForExecutadoUpdate : UsuariosTestes
  {
    private IUserService _service;
    private Mock<IUserService> _serviceMock;

    [Fact(DisplayName = "É possível executar o método Update")]
    public async Task E_Possivel_Executar_Metodo_Update()
    {
      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Post(userDtoCreate)).ReturnsAsync(userDtoCreateResult);
      _service = _serviceMock.Object;

      var _result = await _service.Post(userDtoCreate);
      Assert.NotNull(_result);
      Assert.Equal(NomeUsuario, _result.Name);
      Assert.Equal(EmailUsuario, _result.Email);

      _serviceMock = new Mock<IUserService>();
      _serviceMock.Setup(m => m.Put(userDtoUpdate)).ReturnsAsync(userDtoUpdateResult);
      _service = _serviceMock.Object;

      var _reusltUpdate = await _service.Put(userDtoUpdate);
      Assert.Equal(NomeUsuarioAlterado, _reusltUpdate.Name);
      Assert.Equal(EmailUsuarioAlterado, _reusltUpdate.Email);
    }
  }
}
