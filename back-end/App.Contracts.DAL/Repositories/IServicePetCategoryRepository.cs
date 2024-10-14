using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IServicePetCategoryRepository: IEntityRepository<App.Domain.ServicePetCategory>, IServicePetCategoryRepositoryCustom<App.Domain.ServicePetCategory>
{
    
}
public interface IServicePetCategoryRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllByServiceIdAsync(Guid serviceId);
}