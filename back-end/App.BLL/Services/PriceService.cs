using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class PriceService:
    BaseEntityService<App.Domain.Price, App.BLL.DTO.Price, IPriceRepository>,
    IPriceService
{
    public PriceService(IUnitOfWork uoW, IPriceRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.Price, App.BLL.DTO.Price>(mapper))
    {
    }


}