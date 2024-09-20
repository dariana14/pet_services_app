using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Location: BaseEntityId
{
    [MaxLength(128)]
    public string City { get; set; } = default!;
    
    
    public ICollection<Advertisement>? Advertisements { get; set; }
}