using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IAdvertisementService: IEntityRepository<App.BLL.DTO.Advertisement>, IAdvertisementRepositoryCustom<App.BLL.DTO.Advertisement>
{
    
}