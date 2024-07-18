using AutoMapper;
using Looplex.DotNet.Core.Application.Abstractions.DTOs;
using Looplex.DotNet.Core.Domain;

namespace Looplex.DotNet.Core.Infra.Profiles
{
    public class CoreProfile : Profile
    {
        public CoreProfile()
        {
            CreateMap(typeof(PaginatedCollection<>), typeof(PaginatedCollectionDTO<>))
                .ReverseMap();
        }
    }
}
