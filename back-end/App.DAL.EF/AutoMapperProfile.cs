using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<App.Domain.Category, App.Domain.Category>().ReverseMap();
        CreateMap<App.Domain.PetCategory, App.Domain.PetCategory>().ReverseMap();
        CreateMap<App.Domain.ServicePetCategory, App.Domain.ServicePetCategory>().ReverseMap();
        CreateMap<App.Domain.Service, App.Domain.Service>().ReverseMap();
        CreateMap<App.Domain.Location, App.Domain.Location>().ReverseMap();
        CreateMap<App.Domain.Price, App.Domain.Price>().ReverseMap();
        CreateMap<App.Domain.Status, App.Domain.Status>().ReverseMap();
        CreateMap<App.Domain.Advertisement, App.Domain.Advertisement>().ReverseMap();
    }
}