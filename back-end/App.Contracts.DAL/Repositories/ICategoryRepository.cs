using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface ICategoryRepository: IEntityRepository<App.Domain.Category>, ICategoryRepositoryCustom<App.Domain.Category>
{
    
}
public interface ICategoryRepositoryCustom<TEntity>
{
}