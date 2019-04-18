using AutoMapper;
using DTO.Models;

namespace ServiceLayer.Automapper.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<ActivityDTO, Models.Activity>().ReverseMap();
            CreateMap<InputOperationsDTO, Models.InputOperations>().ReverseMap();
            CreateMap<OutputOperationsDTO, Models.OutputOperations>().ReverseMap();
        }
    }
}
