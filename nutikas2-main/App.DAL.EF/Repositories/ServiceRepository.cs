using App.Contracts.DAL.Repositories;
using App.Domain.Enums;
using AutoMapper;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ServiceRepository: BaseEntityRepository<Domain.Service, Domain.Service, AppDbContext>, IServiceRepository
{
    
    public ServiceRepository(AppDbContext dbContext, IMapper mapper) :
        base(dbContext, new DalDomainMapper<Domain.Service, Domain.Service>(mapper))
    {
    }
    
}