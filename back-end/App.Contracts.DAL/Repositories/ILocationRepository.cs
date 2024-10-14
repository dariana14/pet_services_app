using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ILocationRepository: IEntityRepository<App.Domain.Location>, ILocationRepositoryCustom<App.Domain.Location>
{
    
}
public interface ILocationRepositoryCustom<TEntity>
{
}