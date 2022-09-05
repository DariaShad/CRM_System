using AutoMapper;
using CRM_System.BusinessLayer;
using CRM_System.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[Authorize]
[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class LeadsController : ControllerBase
{
    public ClaimModel _claims;
    private readonly ILeadsService _leadsService;
    private readonly IMapper _mapper;
    private readonly ILogger<LeadsController> _logger;

    public LeadsController(ILeadsService leadsService, IMapper mapper, ILogger<LeadsController> logger)
    {
        _leadsService = leadsService;
        _mapper = mapper;
        _logger = logger;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> Register([FromBody] LeadRegistrationRequest request)
    {
        _logger.LogInformation("Controller: Lead registration");
        var result = await _leadsService.Add(_mapper.Map<LeadDto>(request));
        return Created($"{this.GetUrl()}/{result}", result);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeadMainInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LeadMainInfoResponse>> GetById(int id)
    {
        _logger.LogInformation("Controller: Get lead by id");
        var claims = this.GetClaims();
        var lead = await _leadsService.GetById(id, claims);

        if (lead is null)
            return NotFound();
        else
            return Ok(_mapper.Map<LeadMainInfoResponse>(lead));
    }

    [AuthorizeByRole(Role.Admin)]
    [HttpGet]
    [ProducesResponseType(typeof(List<LeadMainInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<List<LeadMainInfoResponse>>> GetAll()
    {
        _logger.LogInformation("Controller: Get all leads");
        var leads = await _leadsService.GetAll();
        return Ok(_mapper.Map<List<LeadAllInfoResponse>>(leads));
    }

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult> Update([FromBody] LeadUpdateRequest request, int id)
    {
        _logger.LogInformation("Controller: Lead registration");
        var claims = this.GetClaims();

        await _leadsService.Update(_mapper.Map<LeadDto>(request), id, claims);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Remove(int id)
    {
        _logger.LogInformation("Controller: Remove lead");
        var claims = this.GetClaims();

        await _leadsService.DeleteOrRestore(id, true, claims);
        return NoContent();
    }

    [AuthorizeByRole(Role.Admin)]
    [HttpPatch("{id}/restore")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> Restore(int id)
    {
        _logger.LogInformation("Controller: Restore lead");
        var claims = this.GetClaims();

        await _leadsService.DeleteOrRestore(id, false, claims);
        return NoContent();
    }
}
