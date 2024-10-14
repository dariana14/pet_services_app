using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IPetCategoryRepository: IEntityRepository<App.Domain.PetCategory>, IPetCategoryRepositoryCustom<App.Domain.PetCategory>
{
    
}
public interface IPetCategoryRepositoryCustom<TEntity>
{
}