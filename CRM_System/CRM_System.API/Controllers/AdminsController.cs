using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class AdminsController : ControllerBase
{
    private readonly IAdminsService _adminsService;
    private readonly IMapper _mapper;

    public AdminsController (IAdminsService adminsService, IMapper mapper)
    {
        _adminsService = adminsService;
        _mapper = mapper;
    }

    //[AuthorizeByRole(Role.Regular, Role.Vip)]
    //[HttpGet("{email}")]
    //[ProducesResponseType(typeof(LeadMainInfoResponse), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    //public async Task<ActionResult<LeadMainInfoResponse>> GetAdminByEmail(string ema)
    //{

    //}

}
