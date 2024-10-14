using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class ServicePetCategoryService:
    BaseEntityService<App.Domain.ServicePetCategory, App.BLL.DTO.ServicePetCategory, IServicePetCategoryRepository>,
    IServicePetCategoryService
{
    public ServicePetCategoryService(IUnitOfWork uoW, IServicePetCategoryRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.ServicePetCategory, App.BLL.DTO.ServicePetCategory>(mapper))
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.ServicePetCategory>> GetAllByServiceIdAsync(Guid serviceId)
    {
        return (await Repository.GetAllByServiceIdAsync(serviceId)).Select(e => Mapper.Map(e))!;
    }
}