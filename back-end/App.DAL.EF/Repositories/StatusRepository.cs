using App.Contracts.DAL.Repositories;
using App.Domain;
using App.Domain.Enums;
using AutoMapper;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StatusRepository: BaseEntityRepository<App.Domain.Status, App.Domain.Status, AppDbContext>,
    IStatusRepository
{
    public StatusRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Status, App.Domain.Status>(mapper))
    {
    }

    public async Task<IEnumerable<Domain.Status>> GetAllByNameAsync(int name)
    {
        EStatusName statusName = name == 1 ? EStatusName.Active : EStatusName.Paused;
        
        return await RepoDbSet
            .Where(a => a.StatusName == statusName)
            .ToListAsync();
    }
}