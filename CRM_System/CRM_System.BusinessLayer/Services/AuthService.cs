using CRM.DataLayer;
using CRM.DataLayer.Interfaces;
using CRM_System.BusinessLayer.Infrastucture;
using CRM_System.BusinessLayer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CRM_System.BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILeadsRepository _leadsRepository;
        private readonly IAdminRepository _adminRepository;

        public AuthService(ILeadsRepository leadsRepository, IAdminRepository adminRepository)
        {
            _leadsRepository = leadsRepository;
            _adminRepository = adminRepository;
        }

        public async Task<ClaimModel> Login(string login, string password)
        {
            ClaimModel claimModel = new ClaimModel();

            var lead = await _leadsRepository.GetByEmail(login);

            // move to another class
            if (lead is not null && login == lead.Email &&
                PasswordHash.ValidatePassword(password, lead.Password) && !lead.IsDeleted)
            {

                if (lead.Role == Role.Regular)
                {
                    claimModel.Role = Role.Regular;
                    claimModel.Id = lead.Id;
                }
                else 
                    claimModel.Role = Role.Vip;
            }

            var admin = await _adminRepository.GetAdminByEmail(login);

            // move to another class
            if (admin is not null && login == admin.Email &&
                PasswordHash.ValidatePassword(password, admin.Password) && !admin.IsDeleted)
            {
                claimModel.Role = Role.Admin;
                claimModel.Id = admin.Id;
            }

            return claimModel;
        }

        public string GetToken(ClaimModel claimModel)
        {
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
}
