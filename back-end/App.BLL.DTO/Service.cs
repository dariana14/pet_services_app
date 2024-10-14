using App.Domain.Enums;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Service: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string Description { get; set; } = default!;
    
    public Guid CategoryId { get; set; }
}