using App.Domain.Enums;
using Base.Domain;

namespace App.Domain;

public class Status: BaseEntityId
{
    public EStatusName StatusName { get; set; } = default!;
}