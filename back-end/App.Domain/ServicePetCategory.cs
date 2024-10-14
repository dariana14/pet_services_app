using Base.Domain;

namespace App.Domain;

public class ServicePetCategory: BaseEntityId
{
    public Guid ServiceId { get; set; }
    public Service? Service { get; set; }
    
    public Guid PetCategoryId { get; set; }
    public PetCategory? PetCategory { get; set; }
}