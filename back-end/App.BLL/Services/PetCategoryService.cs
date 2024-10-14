using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class PetCategoryService:
    BaseEntityService<App.Domain.PetCategory, App.BLL.DTO.PetCategory, IPetCategoryRepository>,
    IPetCategoryService
{
    public PetCategoryService(IUnitOfWork uoW, IPetCategoryRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.PetCategory, App.BLL.DTO.PetCategory>(mapper))
    {
    }
}