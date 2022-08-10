using CRM_System.BusinessLayer.Models;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace CRM_System.API.Controllers
{
    [ApiController]
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
