using App.Domain.Enums;
using Base.Contracts.Domain;

namespace App.BLL.DTO;

public class PetCategory: IDomainEntityId
{
    public Guid Id { get; set; }

    public string PetCategoryName { get; set; } = default!;
}