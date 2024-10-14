using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class ServicePetCategory: IDomainEntityId
{
    public Guid Id { get; set; }

    public Guid ServiceId { get; set; }
    
    public Guid PetCategoryId { get; set; }
    
    public string? PetCategoryName { get; set; }
}