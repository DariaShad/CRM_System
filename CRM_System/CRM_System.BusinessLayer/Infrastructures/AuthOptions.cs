using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CRM_System.BusinessLayer.Infrastuctures;

public class AuthOptions
{
    public const string Issuer = "MyAuthServer";
    public const string Audience = "MyAuthClient";
    const string Key = "mysupersecret_secretkey!123";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
}
