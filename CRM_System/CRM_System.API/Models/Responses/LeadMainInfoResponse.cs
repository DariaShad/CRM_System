using CRM_System.DataLayer;

namespace CRM_System;
public class LeadMainInfoResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public City City { get; set; }
}
