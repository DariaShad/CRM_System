namespace CRM.DataLayer.Models;

public class LeadDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Passport { get; set; }
    public City City { get; set; }
    public string Address { get; set; }
    public LeadRole Role { get; set; }
    public DateTime RegistrationDate { get; set; }
    public bool IsDeleted { get; set; }

    public override bool Equals(object? obj)
    {
        bool flag = true;
        if (obj == null || !(obj is LeadDto))
        {
            flag = false;
        }
        LeadDto leadDto = (LeadDto)obj;
        if (leadDto.Id != this.Id ||
            leadDto.FirstName != this.FirstName ||
            leadDto.LastName != this.LastName ||
            leadDto.Patronymic != this.Patronymic ||
            leadDto.Birthday != this.Birthday ||
            leadDto.Email != this.Email ||
            leadDto.Phone != this.Phone ||
            leadDto.Passport != this.Passport ||
            leadDto.City != this.City ||
            leadDto.Address != this.Address ||
            leadDto.Role != this.Role ||
            leadDto.RegistrationDate != this.RegistrationDate ||
            leadDto.IsDeleted != this.IsDeleted)
        {
            flag = false;
        }
        return flag;
    }
}
