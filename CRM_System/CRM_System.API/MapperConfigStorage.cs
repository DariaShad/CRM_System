using AutoMapper;
using CRM.DataLayer.Models;

namespace CRM_System.API;

public class MapperConfigStorage : Profile
{
   public MapperConfigStorage()
    {
        CreateMap<LeadRegistrationRequest, LeadDto>();
        CreateMap<LeadUpdateRequest, LeadDto>();
        CreateMap<LeadDto, LeadAllInfoResponse>();
        CreateMap<LeadDto, LeadMainInfoResponse>();
    }
}

