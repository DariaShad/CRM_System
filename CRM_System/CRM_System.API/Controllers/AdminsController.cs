using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[AuthorizeByRole(Role.Admin)]
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

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddAdmin([FromBody] AdminRegistrationRequest adminRegistrationRequest)
    {
        var result = await _adminsService.AddAdmin(_mapper.Map<AdminDto>(adminRegistrationRequest));
        return Created($"{this.GetUrl()}/{result}", result);
    }
}
