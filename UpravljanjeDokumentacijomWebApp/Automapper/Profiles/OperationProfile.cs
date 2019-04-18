using AutoMapper;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpravljanjeDokumentacijomWebApp.Models;

namespace UpravljanjeDokumentacijomWebApp.Automapper.Profiles
{
    public class OperationProfile : Profile
    {
        public OperationProfile()
        {
            CreateMap<OperationViewModel, OperationDTO>()
                .ForMember(dest => dest.Received, opt => opt.MapFrom(src => src.InputReceive))
                .ForMember(dest => dest.Requested, opt => opt.MapFrom(src => src.InputByRequest))
                .ForMember(dest => dest.OutputDocuments, opt => opt.MapFrom(src => src.Output))
                .ReverseMap();
        }
    }
}
