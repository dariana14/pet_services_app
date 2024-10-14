using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class Price: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public decimal Value { get; set; } = default!;
    
}