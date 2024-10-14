using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class LocationService:
    BaseEntityService<App.Domain.Location, App.BLL.DTO.Location, ILocationRepository>,
    ILocationService
{
    public LocationService(IUnitOfWork uoW, ILocationRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.Location, App.BLL.DTO.Location>(mapper))
    {
    }
}