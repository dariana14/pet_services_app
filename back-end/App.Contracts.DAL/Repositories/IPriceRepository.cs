using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IPriceRepository: IEntityRepository<App.Domain.Price>, IPriceRepositoryCustom<App.Domain.Price>
{
    
}
public interface IPriceRepositoryCustom<TEntity>
{
}