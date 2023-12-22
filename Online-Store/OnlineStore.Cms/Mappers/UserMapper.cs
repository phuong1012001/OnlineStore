using AutoMapper;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.Cms.Models.Request.User;
using OnlineStore.Cms.Models.Response.User;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Cms.Mappers
{
    public class UserMapper: Profile
    {
        public UserMapper()
        {
            //Index
            CreateMap<User, UserDto>();
            CreateMap<UserDto, UserRes>();

            //Create
            CreateMap<UserReq, UserDto>();
            CreateMap<UserDto, User>();

            //Edit
            CreateMap<UserDto, UserEditRes>();
            CreateMap<UserEditReq, UserDto>();
        }
    }
}
