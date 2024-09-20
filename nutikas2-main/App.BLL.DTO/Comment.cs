using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Comment: IDomainEntityId
{
    public Guid AppUserId { get; set; }
    
    public string CommentDescription { get; set; } = default!;
    public Guid Id { get; set; }
}