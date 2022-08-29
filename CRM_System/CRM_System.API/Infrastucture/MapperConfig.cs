using AutoMapper;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;

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

