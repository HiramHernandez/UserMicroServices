using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMicroservice.Entities;
using MyMicroservice.DAL.Contracts;

namespace MyMicroservice.DAL;

public class UserRepository: GenericRepository<User>, IUserRepository
{
    private readonly ApiUsersContext _dbContext;

    public UserRepository(ApiUsersContext dbContext): base(dbContext){
        _dbContext = dbContext;
    }

    public async  Task<User> Register(User user)
    {
        User newUser = new User();

        using(var transaction = _dbContext.Database.BeginTransaction())
        {
            try{
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                newUser = user;
                transaction.Commit();
            } catch{
                transaction.Rollback();
                throw;
            }
        }
        return newUser;
    }
}