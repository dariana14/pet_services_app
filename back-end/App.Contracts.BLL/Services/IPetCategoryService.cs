using App.Contracts.DAL.Repositories;
using Base.Contracts.DAL;

namespace App.Contracts.BLL.Services;

public interface IPetCategoryService: IEntityRepository<App.BLL.DTO.PetCategory>, IPetCategoryRepositoryCustom<App.BLL.DTO.PetCategory>
{
    
}