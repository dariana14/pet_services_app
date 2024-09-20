using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IStatusService: IEntityRepository<App.BLL.DTO.Status>, IStatusRepositoryCustom<App.BLL.DTO.Status>
{
    
}