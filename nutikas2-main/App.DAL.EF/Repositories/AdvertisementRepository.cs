using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AdvertisementRepository: BaseEntityRepository<Domain.Advertisement, Domain.Advertisement, AppDbContext>, IAdvertisementRepository
{
    public AdvertisementRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<Domain.Advertisement, Domain.Advertisement>(mapper))
    {
    }

    public new async Task<IEnumerable<Domain.Advertisement>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return await CreateQuery(userId, noTracking)
            .Include(a => a.AppUser)
            .Include(a => a.Location)
            .Include(a => a.Price)
            .Include(a => a.Status)
            .Include(a => a.Service)
            .ThenInclude(s => s!.Category)
            .ToListAsync();
    }

    public new async Task<Domain.Advertisement?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {

        return await CreateQuery(userId, noTracking)
            .Include(a => a.AppUser)
            .Include(a => a.Location)
            .Include(a => a.Price)
            .Include(a => a.Status)
            .Include(a => a.Service)
            .ThenInclude(s => s!.Category)
            .FirstOrDefaultAsync(m => m.Id.Equals(id));
    }
}