using AutoMapper;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.API.Models.Responses;

namespace CRM_System.API;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<AddAccountRequest, AccountDto>();
        CreateMap<UpdateAccountRequest, AccountDto>();
        CreateMap<AccountDto, AccountResponse>();
        CreateMap<AccountDto, AccountsByLeadIdResponse>();
        CreateMap<AccountDto, AllAccountsResponse>();
    }
}
