using App.Domain.Enums;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IServiceRepository: IEntityRepository<App.Domain.Service>, IServiceRepositoryCustom<App.Domain.Service>
{
    
}

public interface IServiceRepositoryCustom<TEntity>
{
}