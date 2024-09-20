using App.BLL.Services;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using AutoMapper;
using Base.Test.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace App.Test.Unit.BLL;

public class AdvertisementServiceTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly AdvertisementService _testAdvertisementService;

    private readonly AppDbContext _ctx;
    
    public AdvertisementServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        // set up mock database - inmemory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    
        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);
    
        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
    
        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<App.Domain.Advertisement, App.BLL.DTO.Advertisement>().ForMember(bllA => bllA.City,
                    options =>
                        options.MapFrom(domA => domA.Location!.City)).ForMember(bllA => bllA.PriceValue,
                    options =>
                        options.MapFrom(domA => domA.Price!.Value)).ForMember(bllA => bllA.Description,
                    options =>
                        options.MapFrom(domA => domA.Service!.Description)).ForMember(bllA => bllA.CategoryName,
                    options =>
                        options.MapFrom(domA => domA.Service!.Category!.CategoryName)).ForMember(
                    bllA => bllA.StatusName,
                    options =>
                        options.MapFrom(domA => domA.Status!.StatusName));
                cfg.CreateMap<App.BLL.DTO.Advertisement, App.Domain.Advertisement>();
            });
        
        var mapper = config.CreateMapper();
    
        _testAdvertisementService =
            new AdvertisementService( 
                new AppUOW(_ctx, mapper), 
                new AdvertisementRepository(_ctx, mapper),
                mapper
            );
    }
    
    
    [Fact]
    public async Task FirstOrDefaultAsync1Test()
    {
        // arrange
        var locationId = Guid.NewGuid();
        var location = new App.Domain.Location()
        {
            Id = locationId,
            City = "testCity",
        };
        var loc = _ctx.Locations.Add(location).Entity;
        _ctx.SaveChanges();
        
        var advertisement = new App.BLL.DTO.Advertisement()
        {
            Id = Guid.NewGuid(),
            Title = "titleTest",
            AppUserId = Guid.NewGuid(),
            LocationId = locationId,
            PriceId = Guid.NewGuid(),
            ServiceId = Guid.NewGuid(),
            StatusId = Guid.NewGuid(),
        };
        
        _testAdvertisementService.Add(advertisement);
        _ctx.SaveChanges();

        // act
        var adv = await _testAdvertisementService.FirstOrDefaultAsync(advertisement.Id);

        // assert
        if (adv != null)
        {
            Assert.NotNull(adv.City);
        }
    }
    
}