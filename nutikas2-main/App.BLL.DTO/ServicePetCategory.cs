using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class ServicePetCategory: IDomainEntityId
{
    public Guid Id { get; set; }

    public Guid ServiceId { get; set; }
    
    public Guid PetCategoryId { get; set; }
    
    public string? PetCategoryName { get; set; }
}