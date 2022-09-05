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

    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost]
    public async Task <string> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _authService.Login(loginRequest.Login, loginRequest.Password);

        _logger.LogInformation("Controllers: Login is successful");

        return _authService.GetToken(user);
    }
}
