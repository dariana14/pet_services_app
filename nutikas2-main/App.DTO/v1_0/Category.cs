using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class Category: IDomainEntityId
{ 
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = default!;
}