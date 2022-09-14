using AutoMapper;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Models;
using CRM_System.DataLayer;
using IncredibleBackendContracts.Enums;
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
    private readonly ILogger<AdminsController> _logger;

    public AdminsController (IAdminsService adminsService, IMapper mapper, ILogger<AdminsController> logger)
    {
        _adminsService = adminsService;
        _mapper = mapper;
        _logger= logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddAdmin([FromBody] LoginRequest adminRegistrationRequest)
    {
        _logger.LogInformation($"Controller: Add an admin: {adminRegistrationRequest.Login}");
        var result = await _adminsService.AddAdmin(_mapper.Map<AdminDto>(adminRegistrationRequest));
        return Created($"{this.GetUrl()}/{result}", result);
    }
}
