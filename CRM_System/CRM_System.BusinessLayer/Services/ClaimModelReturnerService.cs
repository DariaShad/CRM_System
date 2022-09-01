using CRM_System.DataLayer;

namespace CRM_System.BusinessLayer;

public class ClaimModelReturnerService
{
    public static ClaimModel ReturnLead(LeadDto lead, string login, string password, ClaimModel claimModel)
    {
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

        return claimModel;
    }

    public static ClaimModel ReturnAdmin(AdminDto admin, string login, string password, ClaimModel claimModel)
    {
        if (admin is not null && login == admin.Email &&
           PasswordHash.ValidatePassword(password, admin.Password) && !admin.IsDeleted)
        {
            claimModel.Role = Role.Admin;
            claimModel.Id = admin.Id;
        }
        return claimModel;
    }
}
