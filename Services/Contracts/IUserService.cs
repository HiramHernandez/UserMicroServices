using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMicroservice.DTO;

namespace MyMicroservice.Services.Contracts;

public interface IUserService
{
    Task<List<UserDTO>> ListUsers();
    Task<UserDTO> Create(UserDTO user);
    Task<bool> Edit(UserDTO user);
    Task<bool> Remove(int id);
}