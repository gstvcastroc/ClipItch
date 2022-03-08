using API.Models;
using API.ViewModels;
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
        }
    }
}