using AutoMapper;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;

namespace CRM_System.API;

public class MapperConfigStorage : Profile
{
    public MapperConfigStorage()
    {
        CreateMap<AddAccountRequest, AccountDto>();
    }
}
