using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;

namespace DataProvider.Requests
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Candidate, CandidateDTO>().ForMember(can => can.Email, opt => opt.MapFrom(src => src.Account.Email))
            .ForMember(can => can.Password, opt => opt.MapFrom(src => src.Account.Password));
             
        }

     
    }
}
