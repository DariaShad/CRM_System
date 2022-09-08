using CRM_System.DataLayer;
using Microsoft.Extensions.Logging;

namespace CRM_System.BusinessLayer;

public class AccessService
{
    private readonly ILogger<AccessService> _logger;
    
    public AccessService(ILogger<AccessService> logger)
    {
        _logger = logger;
    }

    public static void CheckAccessForLeadAndManager(int id, ClaimModel claims)
    {
        if (claims is not null && claims.Id != id &&
            claims.Role != Role.Admin)
        {
            throw new AccessDeniedException($"Access denied");
        }
    }
}
