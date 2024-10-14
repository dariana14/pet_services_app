using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Base.Domain;

namespace App.Domain;

public class Category: BaseEntityId
{
    [MaxLength(255)]
    public string CategoryName { get; set; } = default!;
    
    public ICollection<Service>? Services { get; set; }

}