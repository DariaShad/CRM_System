using AutoMapper;
using CRM.DataLayer.Models;
using CRM_System.API.Models.Requests;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController (IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;

        }
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public ActionResult <int> AddAccount([FromBody] AddAccountRequest accountRequest)
        {
            var result=_accountService.AddAccount(_mapper.Map<AccountDto>(accountRequest));
            return Created("", result);
        }
    
    }
   
}
