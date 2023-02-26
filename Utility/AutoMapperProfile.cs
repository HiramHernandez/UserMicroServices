using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyMicroservice.DTO;
using MyMicroservice.Entities;
namespace MyMicroservice.Utility;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        #region User
        CreateMap<User, UserDTO>().ReverseMap();
        #endregion user
    }

}