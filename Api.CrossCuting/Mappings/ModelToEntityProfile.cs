using Api.Domain.Entities;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCuting.Mappings
{
  public class ModelToEntityProfile : Profile
  {
    public ModelToEntityProfile()
    {
      CreateMap<UserEntity, UserModel>()
          .ReverseMap();
    }
  }
}
