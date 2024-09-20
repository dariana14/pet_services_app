using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class PetCategory: IDomainEntityId
{
    public Guid Id { get; set; }

    public string PetCategoryName { get; set; } = default!;
}