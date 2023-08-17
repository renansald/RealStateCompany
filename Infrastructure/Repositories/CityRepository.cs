﻿using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly DataContext _dataContext;

    public CityRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    public CityEntity GetById(int id)
    {
        var city = _dataContext.Cities.Find(id) 
                ?? throw new Exception("Not Found");
        return city;
    }

    public void DeleteById(int id)
    {
        try
        {
            var city = _dataContext.Cities.Find(id)
                       ?? throw new Exception("City doesn't exist on database");
            _dataContext.Cities.Remove(city);
            _dataContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int? Create(CityEntity entity)
    {
        try
        {
            _dataContext.Cities.Add(entity);
            
            _dataContext.SaveChanges();
            
            return entity.Id;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void Update(CityEntity entity)
    {
        try
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            _dataContext.SaveChanges();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}