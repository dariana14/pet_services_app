using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IServicePetCategoryService: 
    IEntityRepository<App.BLL.DTO.ServicePetCategory>, 
    IServicePetCategoryRepositoryCustom<App.BLL.DTO.ServicePetCategory>
{
    
}