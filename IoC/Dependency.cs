using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyMicroservice.Entities;
using MyMicroservice.Utility;
using MyMicroservice.DAL;
using MyMicroservice.DAL.Contracts;
using MyMicroservice.Services.Contracts;
using MyMicroservice.Services;
using AutoMapper;

namespace MyMicroservice.IoC
{
    public static class Dependency
    {

        public static void InjectDependency(this IServiceCollection services, IConfiguration configuration)
        {
            //configuration.GetConnectionString("cadenaSQL")
            services.AddDbContext<ApiUsersContext>(opt =>
            {
                opt.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IUserService, UsersService>();
        }

    }
}

