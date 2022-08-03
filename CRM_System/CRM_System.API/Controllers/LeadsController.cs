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
    private readonly ILeadsService _leadService;
    private readonly IMapper _mapper;

    public LeadsController(ILeadsService leadService, IMapper mapper)
    {
        _leadService = leadService;
        _mapper = mapper;
    }

    //[AllowAnonymous]
    //[HttpPost]
    //[ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    //public ActionResult<int> AddClient([FromBody] LeadRegistrationRequest request)
    //{
    //    var result = _leadService.Add(_mapper.Map<LeadDto>(request));
    //    return Created($"{this.GetUrl()}/{result}", result);
    //}

    [AuthorizeByRole(Role.Regular, Role.Vip)]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LeadAllInfoResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<LeadAllInfoResponse> GetById(int id)
    {
        var result = _leadService.GetById(id);
        if (result is null)
            return NotFound();
        else
            return Ok(_mapper.Map<LeadAllInfoResponse>(result));
    }

    [AuthorizeByRole]
    [HttpGet]
    [ProducesResponseType(typeof(List<LeadAllInfoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public ActionResult<List<LeadAllInfoResponse>> GetAll()
    {
        var leads = _leadService.GetAll();
        return Ok(_mapper.Map<List<LeadAllInfoResponse>>(leads));
    }

    //[AuthorizeByRole(Role.Lead)]
    //[HttpPut("{id}")]
    //[ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    //public ActionResult Update([FromBody] LeadUpdateRequest request, int id)
    //{
    //    _leadService.Update(_mapper.Map<LeadDto>(request), id);
    //    return Ok();
    //}

    //[AuthorizeByRole(Role.Lead)]
    //[HttpDelete]
    //[ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    //[ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    //public ActionResult Remove(int id)
    //{
    //    _leadService.Remove(id, true);
    //    return NoContent();
    //}

    //+ restore

}
