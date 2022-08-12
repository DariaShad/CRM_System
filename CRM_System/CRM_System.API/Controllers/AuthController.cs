using AutoMapper;
using CRM.DataLayer;
using CRM.DataLayer.Models;
using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Models;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public string Login([FromBody] LoginRequest loginRequest)
        {
            var user = _authService.Login(loginRequest);

            return _authService.GetToken(user);
        }
    }
}
