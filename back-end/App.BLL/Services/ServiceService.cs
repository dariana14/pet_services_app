using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using App.Domain.Enums;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class ServiceService: BaseEntityService<App.Domain.Service, App.BLL.DTO.Service, IServiceRepository>,
IServiceService
{
    public ServiceService(IUnitOfWork uoW, IServiceRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.Service, App.BLL.DTO.Service>(mapper))
    {
    }
}