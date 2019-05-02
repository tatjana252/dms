using AutoMapper;
using DTO.Models;
using Models;
using System.IO;

namespace DocumentManagement.Automapper.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDTO, Models.Document>().ReverseMap();
            CreateMap<DocumentInfo, Document>().ReverseMap();
            CreateMap<DocumentInfo, DocumentDTO>().ReverseMap();
            CreateMap<FileWrapperDTO, Models.FileWrapper>().ReverseMap();

            CreateMap<DocumentDTO, DocumentCreatedEvent>().ReverseMap();
            CreateMap<DocumentDTO, UpdateDocumentCommand>().ReverseMap();
        }
    }
}
