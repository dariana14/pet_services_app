using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class CategoryRepository: BaseEntityRepository<App.Domain.Category, App.Domain.Category, AppDbContext>,
    ICategoryRepository
{
    public CategoryRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Category, App.Domain.Category>(mapper))
    {
    }
}