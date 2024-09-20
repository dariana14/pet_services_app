using App.Domain.Enums;
using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class Status: IDomainEntityId
{
    public Guid Id { get; set; }

    public EStatusName StatusName { get; set; } = default!;
    
}