using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Price: BaseEntityId
{
    public decimal Value { get; set; } = default!;
    
    public ICollection<Advertisement>? Advertisements { get; set; }
    
}