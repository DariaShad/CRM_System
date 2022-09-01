using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public class AccessService
{
    public static void CheckAccessForLeadAndManager(int id, ClaimModel claims)
    {
        if (claims is not null && claims.Id != id &&
            claims.Role != Role.Admin)
        {
            throw new AccessDeniedException($"Access denied");
        }
    }
}
