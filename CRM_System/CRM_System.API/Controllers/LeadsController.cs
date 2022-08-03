using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadsService _leadsService;
    private readonly IMapper _mapper;

    public LeadsController(ILeadsService leadsService, IMapper mapper)
    {
        _leadsService = leadsService;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> Add([FromBody] LeadRegistrationRequest request)
    {
        var result = await _leadsService.Add(_mapper.Map<LeadDto>(request));
        return Created($"{this.GetUrl()}/{result}", result);
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeadMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeadMainInfoResponse>> GetById(int id)
    {
        var result = await _leadsService.GetById(id);

        if (result is null)
            return NotFound();
        else
            return Ok(_mapper.Map<LeadMainInfoResponse>(result));
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpGet]
    [ProducesResponseType(typeof(List<LeadMainInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<LeadMainInfoResponse>>> GetAll()
    {
        var leads = await _leadsService.GetAll();
        return Ok(_mapper.Map<List<LeadAllInfoResponse>>(leads));
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Update([FromBody] LeadUpdateRequest request)
    {
        await _leadsService.Update(_mapper.Map<LeadDto>(request));
        return Ok();
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpDelete]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Remove(int id)
    {
        await _leadsService.DeleteOrRestore(id, true);
        return NoContent();
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPatch("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Restore(int id)
    {
        await _leadsService.DeleteOrRestore(id, false);
        return NoContent();
    }
}
