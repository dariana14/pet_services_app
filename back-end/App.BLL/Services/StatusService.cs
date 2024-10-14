using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;
using AutoMapper;
using Base.BLL;
using Base.Contracts.DAL;

namespace App.BLL.Services;

public class StatusService:
    BaseEntityService<App.Domain.Status, App.BLL.DTO.Status, IStatusRepository>,
    IStatusService
{
    public StatusService(IUnitOfWork uoW, IStatusRepository repository, IMapper mapper) 
        : base(uoW, repository, new BllDalMapper<App.Domain.Status, App.BLL.DTO.Status>(mapper))
    {
    }
    
    public async Task<IEnumerable<App.BLL.DTO.Status>> GetAllByNameAsync(int name)
    {
        return (await Repository.GetAllByNameAsync(name)).Select(e => Mapper.Map(e))!;
    }


}