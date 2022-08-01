using CRM.DataLayer;
using System.ComponentModel.DataAnnotations;

namespace CRM_System.API;

public class LeadUpdateRequest
{
    //добавить ApiErrorMessage

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    public string? Patronymic { get; set; }

    [Required]
    public DateTime Birthday { get; set; }

    [Required]
    [MaxLength(12)]
    public string? Phone { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public string? Passport { get; set; }

    [Required]
    public City City { get; set; }

    [Required]
    public string? Address { get; set; }
}
