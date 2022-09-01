using AutoMapper;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;
using CRM_System.DataLayer;

namespace CRM_System.API;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<LeadRegistrationRequest, LeadDto>();
        CreateMap<LeadUpdateRequest, LeadDto>();
        CreateMap<LeadDto, LeadAllInfoResponse>();
        CreateMap<LeadDto, LeadMainInfoResponse>();

        CreateMap<AddAccountRequest, AccountDto>();
        CreateMap<AddAccountRequest, UpdateAccountRequest>();
        CreateMap<UpdateAccountRequest, AddAccountRequest>();
        CreateMap<AccountDto, AddAccountRequest >();
        CreateMap<UpdateAccountRequest, AccountDto>();
        CreateMap<AccountDto, AccountResponse>();

    }
}

