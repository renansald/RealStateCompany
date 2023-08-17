using Domain.Business.City;
using Domain.Business.Interfaces;
using Domain.DTOs;
using Domain.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Injections;

public static class Injections
{
    public static void ConfigureService(this IServiceCollection service)
    {
        service.AddScoped<ICityRepository, CityRepository>();
        service.AddScoped<ICityBusiness, CityBusiness>();
    }
}