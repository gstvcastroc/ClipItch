using API.Models;
using API.ViewModels.Clips;
using API.ViewModels.Games;
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
      CreateMap<Clip, ClipsViewModel>().ReverseMap();
      CreateMap<Game, GameViewModel>().ReverseMap();
    }
  }
}