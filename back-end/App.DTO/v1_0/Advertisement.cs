using App.Domain.Enums;
using Base.Contracts.Domain;

namespace App.DTO.v1_0;

public class Advertisement: IDomainEntityId
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    
    public Guid PriceId { get; set; }
    
    public Guid LocationId { get; set; }
    
    public Guid ServiceId { get; set; }
    
    public Guid StatusId { get; set; }
    

    public string? City { get; set; }
    
    public decimal? PriceValue { get; set; }
    
    public string? Description { get; set; }
    
    public EStatusName? StatusName { get; set; }
    
    public string? CategoryName { get; set; }
    
}