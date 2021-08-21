using Api.Domain.Models;
using AutoMapper;
using Api.Domain.Dtos.User;

namespace Api.CrossCuting.Mappings
{
  public class DtoToModelProfile : Profile
  {
    public DtoToModelProfile()
    {
      CreateMap<UserModel, UserDto>()
        .ReverseMap();

      CreateMap<UserModel, UserDtoCreate>()
       .ReverseMap();

      CreateMap<UserModel, UserDtoUpdate>()
      .ReverseMap();
    }
  }
}
