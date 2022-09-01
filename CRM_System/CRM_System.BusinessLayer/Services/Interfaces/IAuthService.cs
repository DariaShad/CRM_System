namespace CRM_System.BusinessLayer;

public interface IAuthService
{
    Task<ClaimModel> Login(string login, string password);
    string GetToken(ClaimModel claimModel);
}
