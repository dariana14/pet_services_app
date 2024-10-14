using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class PetCategory: BaseEntityId
{
    [MaxLength(255)]
    public string PetCategoryName { get; set; } = default!;
    
    public ICollection<ServicePetCategory>? ServicePetCategories { get; set; }
}