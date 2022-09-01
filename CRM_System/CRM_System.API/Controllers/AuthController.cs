using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[AllowAnonymous]
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
    public async Task <string> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _authService.Login(loginRequest.Login, loginRequest.Password);

        return _authService.GetToken(user);
    }
}
