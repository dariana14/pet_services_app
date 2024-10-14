using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Location: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string City { get; set; } = default!;
    
    public string Country { get; set; } = default!;
}