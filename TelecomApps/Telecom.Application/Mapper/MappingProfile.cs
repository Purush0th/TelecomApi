using AutoMapper;
using Telecom.Domain.Dtos;
using Telecom.Domain.Models;

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
