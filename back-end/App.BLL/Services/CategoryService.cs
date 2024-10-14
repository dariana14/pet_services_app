using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class CategoryService:
    BaseEntityService<App.Domain.Category, App.BLL.DTO.Category, ICategoryRepository>,
    ICategoryService
{
    public CategoryService(IUnitOfWork uoW, ICategoryRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.Category, App.BLL.DTO.Category>(mapper))
    {
    }
}