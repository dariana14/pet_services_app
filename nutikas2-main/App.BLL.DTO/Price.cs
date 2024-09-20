using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Price: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public decimal Value { get; set; } = default!;
    
}