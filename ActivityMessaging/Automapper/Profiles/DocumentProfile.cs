using AutoMapper;
using DTO.Models;
using System.IO;

namespace DocumentManagement.Automapper.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDTO, Models.Document>().ReverseMap();
            CreateMap<FileWrapperDTO, Models.FileWrapper>().ReverseMap();
        }
    }
}
