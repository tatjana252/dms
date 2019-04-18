using AutoMapper;
using DTO.Models;
using UpravljanjeDokumentacijomWebApp.Models;

namespace UpravljanjeDokumentacijomWebApp.Automapper.Profiles
{
    public class ActivityProfile : Profile
    {
        public ActivityProfile()
        {
            CreateMap<ActivityViewModel, ActivityDTO>().ReverseMap();

        }
    }
}
