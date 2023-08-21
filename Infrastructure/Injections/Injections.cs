using Domain.Business.City;
using Domain.Business.FurnishingType;
using Domain.Business.Interfaces;
using Domain.Business.PropertyType;
using Domain.Business.User;
using Domain.DTOs;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Injections;

public static class Injections
{
    public static void ConfigureService(this IServiceCollection service)
    {
        //Repositories Injections
        service.AddScoped<ICityRepository, CityRepository>();
        
        service.AddScoped<IUserRepository, UserRepository>();
        
        service.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();

        service.AddScoped<IFurnishingTypeRepository, FurnishingTypeRepository>();
        
        //Business Injections
        service.AddScoped<ICityBusiness, CityBusiness>();

        service.AddScoped<IUserBusiness, UserBusiness>();

        service.AddScoped<IPropertyTypeBusiness, PropertyTypeBusiness>();

        service.AddScoped<IFurnishingTypeBusiness, FurnishingTypeBusiness>();
    }
}