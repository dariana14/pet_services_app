using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IPriceService: IEntityRepository<App.BLL.DTO.Price>, IPriceRepositoryCustom<App.BLL.DTO.Price>
{
    
}