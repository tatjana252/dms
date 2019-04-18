using AutoMapper;
using DTO.Models;
using System.IO;
using UpravljanjeDokumentacijomWebApp.Models;

namespace UpravljanjeDokumentacijomWebApp.Automapper.Profiles
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentViewModel, DocumentDTO>()
                .ForMember(dest => dest.InputOperation, opt => opt.MapFrom(src => src.InputOperationViewModel))
                .ForMember(dest => dest.OutputOperation, opt => opt.MapFrom(src => src.OutputOperationViewModel))
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.Document))
                .ReverseMap();

            CreateMap<InputOperationsViewModel, InputOperationsDTO>().ReverseMap();
            CreateMap<OutputOperationsViewModel, OutputOperationsDTO>().ReverseMap();

            CreateMap<FormFileWrapper, FileWrapperDTO>().ConvertUsing((src, dest) =>
            {

                FileWrapperDTO dto = new FileWrapperDTO
                {
                    Name = src.Name
                };
                if (src.File != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        src.File.CopyTo(ms);
                        dto.File = ms.ToArray();
                    }
                }
                return dto;
            });


            CreateMap<FileWrapperDTO, FormFileWrapper>().ConvertUsing((src, dest) =>
            {
                FormFileWrapper ffw = new FormFileWrapper
                {
                    Name = src.Name
                };
                // iformfile ?!
                return ffw;
            });
        }
    }
}
