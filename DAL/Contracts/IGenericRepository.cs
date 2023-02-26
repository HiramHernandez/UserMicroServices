using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using MyMicroservice.Entities;

namespace MyMicroservice.DAL.Contracts;

public interface IGenericRepository<TModel> where TModel:class
{
    Task<TModel> GetAll(Expression<Func<TModel, bool>> filter);

    Task<TModel> Create(TModel model);
    Task<bool> Edit(TModel model);
    Task<bool> Remove(TModel model);
    Task<IQueryable<TModel>> Query(Expression<Func<TModel, bool>> filter=null);
}