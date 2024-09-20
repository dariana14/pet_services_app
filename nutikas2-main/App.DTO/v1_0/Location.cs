using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class Location: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string City { get; set; } = default!;
    
    public string Country { get; set; } = default!;
}