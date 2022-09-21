namespace CRM_System.DataLayer;

public static class StoredProcedures
{
    public const string Lead_Add = "Lead_Add";
    public const string Lead_GetAll = "Lead_GetAll";
    public const string Lead_GetById = "Lead_GetById";
    public const string Lead_Update = "Lead_Update";
    public const string Lead_UpdateRole = "Lead_UpdateRole";
    public const string Lead_Delete = "Lead_Delete";
    public const string Lead_Restore = "Lead_Restore";
    public const string Lead_GetAllInfoByLeadId = "Lead_GetAllInfoByLeadId";
    public const string Lead_GetLeadByEmail = "Lead_GetLeadByEmail";

    public const string Account_Add = "Account_Add";
    public const string Account_GetAll = "Account_GetAll";
    public const string Account_GetById = "Account_GetById";
    public const string Account_Update = "Account_Update";
    public const string Account_Delete = "Account_Delete";
    public const string Account_GetAllAccountsByLeadId = "Account_GetAllAccountsByLeadId";

    public const string Admin_GetAdminByEmail = "Admin_GetAdminByEmail";
    public const string Admin_Add = "Admin_Add";
}
