using CRM.DataLayer.Interfaces;
using CRM_System.Business_Layer.Infrastucture;
using CRM_System.BusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using CRM_System.BusinessLayer.Models;

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

        }
    }
}
