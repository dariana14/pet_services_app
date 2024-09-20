using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class LocationRepository: BaseEntityRepository<App.Domain.Location, App.Domain.Location, AppDbContext>,
    ILocationRepository
{
    public LocationRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Location, App.Domain.Location>(mapper))
    {
    }
}