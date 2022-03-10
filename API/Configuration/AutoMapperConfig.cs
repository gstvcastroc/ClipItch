using API.Models;
using AutoMapper;

namespace API.Configuration
{
  public class AutoMapperConfig : Profile
  {
    public AutoMapperConfig()
    {
      MapModelToViewModel();
    }

    private void MapModelToViewModel()
    {
      CreateMap<Clip, Clip>().ReverseMap();
      CreateMap<Game, Game>().ReverseMap();
    }
  }
}