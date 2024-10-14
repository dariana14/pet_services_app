using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ServicePetCategoryRepository: BaseEntityRepository<App.Domain.ServicePetCategory, App.Domain.ServicePetCategory, AppDbContext>,
    IServicePetCategoryRepository
{
    public ServicePetCategoryRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.ServicePetCategory, App.Domain.ServicePetCategory>(mapper))
    {
    }
    
    public async Task<IEnumerable<Domain.ServicePetCategory>> GetAllByServiceIdAsync(Guid serviceId)
    {
        return await RepoDbSet
            .Include(a => a.PetCategory)
            .Where(a => a.ServiceId == serviceId)
            .ToListAsync();
    }
}