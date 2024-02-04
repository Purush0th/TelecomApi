using AutoMapper;
using Telecom.Domain.Models;
using Telecom.Domain.ViewModels;

namespace Telecom.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Beneficiary, BeneficiaryDto>().ReverseMap();
        }
    }
}
