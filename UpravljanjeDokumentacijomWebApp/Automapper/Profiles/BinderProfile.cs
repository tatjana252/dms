using AutoMapper;
using DTO.Models;
using Models;
using System.Collections.Generic;
using UpravljanjeDokumentacijomWebApp.Models;

namespace UpravljanjeDokumentacijomWebApp.Automapper.Profiles
{
    public class BinderProfile : Profile
    {
        public BinderProfile()
        {
            CreateMap<Binder, BinderViewModel>().ForMember(dest => dest.Outputs, opt => opt.ConvertUsing(new BindedDocumentsListFormatter()));
            CreateMap<BinderViewModel, Binder>().ForMember(dest => dest.Outputs, opt => opt.ConvertUsing(new DictionaryListFormatter()));

        }
    }

    class BindedDocumentsListFormatter : IValueConverter<Dictionary<string, STATE>, List<BindedDocumentVM>>
    {
        public List<BindedDocumentVM> Convert(Dictionary<string, STATE> sourceMember, ResolutionContext context)
        {
            List<BindedDocumentVM> result = new List<BindedDocumentVM>();
            foreach (KeyValuePair<string, STATE> p in sourceMember)
            {
                result.Add(new BindedDocumentVM { State = (STATEVM)p.Value, Type = p.Key });
            }
            return result;
        }
    }

    class DictionaryListFormatter : IValueConverter<List<BindedDocumentVM>, Dictionary<string, STATE>>
    {

        public Dictionary<string, STATE> Convert(List<BindedDocumentVM> sourceMember, ResolutionContext context)
        {
            Dictionary<string, STATE> result = new Dictionary<string, STATE>();
            foreach (BindedDocumentVM p in sourceMember)
            {
                result.Add(p.Type, (STATE)p.State);
            }
            return result;
        }
    }
}
