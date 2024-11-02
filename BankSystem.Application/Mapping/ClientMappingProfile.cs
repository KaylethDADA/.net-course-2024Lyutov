using AutoMapper;
using BankSystem.Application.Dto.ClientDto;
using BankSystem.Domain.Models;
using System.Linq.Expressions;

namespace BankSystem.Application.Mapping
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            CreateMap<GetClientFilterRequest, Expression<Func<Client, bool>>>()
               .ConvertUsing((request, _) =>
                   x => (request.ClientId == null || x.Id == request.ClientId) &&
                        (string.IsNullOrEmpty(request.Search) ||
                        (x.FirstName + " " + x.LastName + " " + x.PassportNumber + " " + x.PhoneNumber).Contains(request.Search)) &&
                        (request.BirthDay == null || x.BirthDay == request.BirthDay));

            CreateMap<CreateClientRequest, Client>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));

            CreateMap<UpdateClientRequest, Client>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.BirthDay, opt => opt.MapFrom(src => src.BirthDay))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));

            CreateMap<Client, ClientResponse>()
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber));
        }
    }
}
