using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMicroservice.DAL.Contracts;
using MyMicroservice.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace MyMicroservice.DAL;

public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
{
    private readonly ApiUsersContext _dbContext;

    public GenericRepository(ApiUsersContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TModel> GetAll(Expression<Func<TModel, bool>> filter)
    {
        try
            {
                TModel model = await _dbContext.Set<TModel>().FirstOrDefaultAsync(filter);
                return model;
            }
            catch
            {
                throw;
            }
    }

    
    public async  Task<TModel> Create(TModel model)
    {
        try
            {
                 _dbContext.Set<TModel>().Add(model);
                await _dbContext.SaveChangesAsync();
                return model;
            }
            catch
            {
                throw;
            }
    }

    public async Task<bool> Edit(TModel model)
    {
        try
            {
                _dbContext.Set<TModel>().Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        catch{ throw; }
    }

    public async Task<bool> Remove(TModel model)
    {
        try
            {
                _dbContext.Set<TModel>().Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
    }

    public async Task<IQueryable<TModel>> Query(Expression<Func<TModel, bool>> filter = null)
    {
        try
            {
                IQueryable<TModel> queryModel = filter == null ? _dbContext.Set<TModel>() : _dbContext.Set<TModel>().Where(filter);
                return queryModel;
            }
        catch{ throw; }
    }
    
}