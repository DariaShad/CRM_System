namespace CRM_System.BusinessLayer.Services.Interfaces;

public interface IAuthService
{
    Task<ClaimModel> Login(string login, string password);
    string GetToken(ClaimModel claimModel);
}
