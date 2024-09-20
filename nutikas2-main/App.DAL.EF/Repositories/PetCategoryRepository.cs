using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PetCategoryRepository: BaseEntityRepository<App.Domain.PetCategory, App.Domain.PetCategory, AppDbContext>,
    IPetCategoryRepository
{
    public PetCategoryRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.PetCategory, App.Domain.PetCategory>(mapper))
    {
    }
}