using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using App.DAL.DTO;
using App.Domain.Enums;
using AutoMapper;
using Base.BLL;
using Base.Contracts.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class AdvertisementService:
    BaseEntityService<App.Domain.Advertisement, App.BLL.DTO.Advertisement, IAdvertisementRepository>,
    IAdvertisementService
{
    public AdvertisementService(IUnitOfWork uoW, IAdvertisementRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.Advertisement, App.BLL.DTO.Advertisement>(mapper))
    {
    }


    public new async Task<IEnumerable<Advertisement>> GetAllAsync(Guid userId = default, bool noTracking = true)
    {
        return (await Repository.GetAllAsync(userId)).Select(e => Mapper.Map(e))!;
    }

    public new async Task<Advertisement?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
    {
        return Mapper.Map(await Repository.FirstOrDefaultAsync(id, userId, noTracking));
    }
}