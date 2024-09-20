﻿using App.Domain.Enums;
using Base.Contracts.DAL;

namespace App.Contracts.DAL.Repositories;

public interface IAdvertisementRepository: IEntityRepository<App.Domain.Advertisement>, IAdvertisementRepositoryCustom<App.Domain.Advertisement>
{
    
}
public interface IAdvertisementRepositoryCustom<TEntity>
{
    // Task<IEnumerable<TEntity>> GetAllAsync1(Guid userId = default, bool noTracking = true);
}