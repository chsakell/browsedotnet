using AutoMapper;
using BrowseDotNet.Domain;
using BrowseDotNet.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Solution, SolutionViewModel>()
                .ForMember(vm => vm.Keys, 
                opt => opt.MapFrom(src => GetCommaSeparatedKeys(src.Keys)));

            Mapper.CreateMap<Snippet, SnippetViewModel>()
                .ForMember(vm => vm.Keys,
                opt => opt.MapFrom(src => GetCommaSeparatedKeys(src.Keys)));

            Mapper.CreateMap<ProgrammingLanguage, ProgrammingLanguageViewModel>()
                .ForMember(vm => vm.NumberOfSnippets,
                opt => opt.MapFrom(src => src.Snippets.Count));

            Mapper.CreateMap<SolutionType, SolutionTypeViewModel>()
                .ForMember(vm => vm.NumberOfSolutions,
                opt => opt.MapFrom(src => src.Solutions.Count));

            Mapper.CreateMap<SearchKey, SearchKeyViewModel>();
        }

        private string GetCommaSeparatedKeys(ICollection<SearchKey> keys)
        {
            if (keys.Count == 0)
                return string.Empty;
            else
            {
                string keysResult = string.Empty;

                foreach (SearchKey key in keys)
                {
                    keysResult += key.Term + ",";
                }

                return keysResult.Substring(0, keysResult.Length - 1);
            }
        }

    }
}