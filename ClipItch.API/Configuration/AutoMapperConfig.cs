using ClipItch.API.Models;
using ClipItch.API.ViewModels;
using AutoMapper;

namespace ClipItch.API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            MapModelToViewModel();
        }

        private void MapModelToViewModel()
        {
            CreateMap<Clipe, ClipesViewModel>().ReverseMap();
        }
    }
}