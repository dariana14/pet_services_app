using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class PriceRepository: BaseEntityRepository<App.Domain.Price, App.Domain.Price, AppDbContext>,
    IPriceRepository
{
    public PriceRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<App.Domain.Price, App.Domain.Price>(mapper))
    {
    }
}