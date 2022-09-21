using CRM_System.BusinessLayer;
using CRM_System.BusinessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

[AllowAnonymous]
[ApiController]
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
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task <ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _authService.Login(loginRequest.Login, loginRequest.Password);

        _logger.LogInformation($"Controllers: Login is successful for {loginRequest.Login}");

        return Ok(_authService.GetToken(user));
    }
}
