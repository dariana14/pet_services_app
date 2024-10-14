using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IServiceService: IEntityRepository<App.BLL.DTO.Service>, IServiceRepositoryCustom<App.BLL.DTO.Service>
{
    
}