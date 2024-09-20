using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Service: BaseEntityId
{
    [MaxLength(10240)]
    public string Description { get; set; } = default!;
    
    public ICollection<Advertisement>? Advertisements { get; set; }
    
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; } 
    
    public ICollection<ServicePetCategory>? ServicePetCategories { get; set; }
}