using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IStatusRepository: IEntityRepository<App.Domain.Status>, IStatusRepositoryCustom<App.Domain.Status>
{
    
}
public interface IStatusRepositoryCustom<TEntity>
{
    
    Task<IEnumerable<TEntity>> GetAllByNameAsync(int statusName);
}