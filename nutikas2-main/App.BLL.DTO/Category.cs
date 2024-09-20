using App.Domain.Enums;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class Category: IDomainEntityId
{ 
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = default!;
}