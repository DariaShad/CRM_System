using IncredibleBackendContracts.Enums;
using Microsoft.AspNetCore.Authorization;

namespace CRM_System.API;

public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    public AuthorizeByRoleAttribute(params Role[] roles)
    {
        Roles = string.Join(",", roles);
    }
}
