using CRM_System.DataLayer;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM_System.BusinessLayer;

public class AuthService : IAuthService
{
    private readonly ILeadsRepository _leadsRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(ILeadsRepository leadsRepository, IAdminRepository adminRepository, ILogger<AuthService> logger)
    {
        _leadsRepository = leadsRepository;
        _adminRepository = adminRepository;
        _logger= logger;
    }

    public async Task<ClaimModel> Login(string login, string password)
    {
        _logger.LogInformation($"Business layer: Database query for login {login}") ;
        ClaimModel claimModel = new ClaimModel();

        var lead = await _leadsRepository.GetByEmail(login);

        ClaimModelReturnerService.ReturnLead(lead, login, password, claimModel);

        var admin = await _adminRepository.GetAdminByEmail(login);

        ClaimModelReturnerService.ReturnAdmin(admin, login, password, claimModel);

        return claimModel;
    }

    public string GetToken(ClaimModel claimModel)
    {
        _logger.LogInformation($"Business layer: Database query for getting token");
        if (claimModel is null)
            throw new DataException("There are empty properties");

        var claims = new List<Claim>
        {
            { new Claim (ClaimTypes.Role, claimModel.Role.ToString()) },
            { new Claim (ClaimTypes.NameIdentifier, claimModel.Id.ToString()) }
        };

        var jwt = new JwtSecurityToken(
            issuer: TokenOptions.Issuer,
            audience: TokenOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
            signingCredentials: new SigningCredentials(TokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
