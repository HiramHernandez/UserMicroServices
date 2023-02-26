using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMicroservice.Services.Contracts;
using MyMicroservice.DTO;
using MyMicroservice.Utility;

namespace MyMicroservice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {
        var resp = new Response<List<UserDTO>>();
        try{
            resp.status = true;
            resp.value = await _userService.ListUsers();
            resp.msg = "success";
        }catch(Exception ex){ 
            resp.status = false;
            resp.msg = ex.Message;  
        }
        return Ok(resp);
    }

    [HttpPost]
    [Route("Save")]
    public async Task<IActionResult> Guardar([FromBody] UserDTO user)
    {
        var rsp = new Response<UserDTO>();
        try
        {
            user.Password = Security.EncryptText(user.Password, "4m4n3c3r", "MD5", 22, "1234567891234567", 128);
            Console.WriteLine($"La password encriptada: {user.Password}");
            rsp.status = true;
            rsp.value = await _userService.Create(user);

        }
        catch (Exception ex)
        {
            rsp.status = false;
            rsp.msg = ex.Message;
        }
        return Ok(rsp);
    }


}