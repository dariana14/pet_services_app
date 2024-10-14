using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class Service: IDomainEntityId
{
    public Guid Id { get; set; } 
    
    public string Description { get; set; } = default!;
    
    public Guid CategoryId { get; set; }
}