using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMicroservice.Entities;

namespace MyMicroservice.DAL.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> Register(User user);
}
