using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyMicroservice.DTO;
using MyMicroservice.DAL.Contracts;
using MyMicroservice.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace MyMicroservice.Services;

public class UsersService : IUserService
{

    private readonly IGenericRepository<User> _userRepository;
    private readonly IMapper _mapper;

    public UsersService(IGenericRepository<User> userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<UserDTO>> ListUsers()
    {
        try{
            var queryUsers = await _userRepository.Query();
            var listUsers = queryUsers.ToList();
            return _mapper.Map<List<UserDTO>>(listUsers.ToList());
        } catch{ throw; }
    }

    public async Task<UserDTO> Create(UserDTO model)
    {
        try
        {
            var newUser = await _userRepository.Create(_mapper.Map<User>(model));
            if (newUser.Id == 0)
                throw new TaskCanceledException("No se pudo cancelar");
            return _mapper.Map<UserDTO>(newUser);
        }
        catch { throw; }
    }

    public async Task<bool> Editar(UserDTO model)
    {
        try
        {
            var userModel = _mapper.Map <User>(model);
            var userFound = await _userRepository.Query(u => u.Id == userModel.Id);
            if (userFound == null)
                throw new TaskCanceledException("Producto no encontrado");
            /*userFound.FirstName = userModel.FirstName;
            userFound.LastName = userModel.LastName;
            userFound.Username = userModel.Username;
            userFound.Password = userModel.Password;
            userFound.EnrollmentDate = userModel.EnrollmentDate;

            bool resp = await _userRepository.Edit(userFound);*/
            bool resp = true;

            if (!resp)
                throw new TaskCanceledException("No se puedo editar");
            return resp;
        }catch { throw; }
    }

    public async Task<bool> Remove(int id)
    {
        try
        {
            var userFound = await _userRepository.GetAll(p => p.Id == id);

            if(userFound == null)
                throw new TaskCanceledException("Producto no existe");

            bool resp = await _userRepository.Remove(userFound);
        
            if (!resp)
                throw new TaskCanceledException("No se puedo eliminar");
                return resp;
        }catch { throw; }
    }

    Task<bool> IUserService.Edit(UserDTO user)
    {
        throw new NotImplementedException();
    }
}