using CRM.DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using CRM_System.BusinessLayer.Models;
using CRM_System.BusinessLayer.Infrastuctures;
using System.Data;
using CRM.DataLayer;

namespace CRM_System.BusinessLayer.Services
{
    public class AuthService
    {
        private readonly ILeadsRepository _leadsRepository;

        public AuthService(ILeadsRepository leadsRepository)
        {
            _leadsRepository = leadsRepository;
        }

        public string GetToken(ClaimModel claimModel)
        {
            if (claimModel is null || claimModel.Email is null)
            {
                throw new DataException("There are empty properties");
            }

            var claims = new List<Claim>
            {
                { new Claim (ClaimTypes.Email, claimModel.Email) },
                { new Claim (ClaimTypes.Role, claimModel.Role.ToString()) },
                { new Claim (ClaimTypes.NameIdentifier, claimModel.Id.ToString()) }
            };

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public ClaimModel Login (LoginRequest loginRequest)
        {
            ClaimModel claimModel = new ClaimModel ();

            var lead = _leadsRepository.GetByEmail(loginRequest.Login);

            if (lead is not null && loginRequest.Login == lead.Result.Email && 
                PasswordHash.ValidatePassword(loginRequest.Password, lead.Result.Password) && !lead.Result.IsDeleted)

            {
                claimModel.Email = loginRequest.Login;
                if (lead.Result.Role == Role.Regular)

                {

                    claimModel.Role = Role.Regular;

                }

                else claimModel.Role = Role.Vip;

            }
            return claimModel;
        }
    }
}
