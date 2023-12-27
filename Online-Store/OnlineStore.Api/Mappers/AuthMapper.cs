using AutoMapper;
using OnlineStore.Api.Models.Requests.Auth;
using OnlineStore.Api.Models.Responses.Auth;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Api.Mappers
{
    public class AuthMapper : Profile
    {
       public AuthMapper()
        {
            CreateMap<LoginReq, LoginDto>();

            CreateMap<AuthResultDto, LoginRes>();

            CreateMap<RegisterReq, UserDto>();

            CreateMap<UserDto, User>();

            CreateMap<AuthResultDto, RegisterRes>();

            CreateMap<ChangePasswordReq, ChangePasswordDto>();

            CreateMap<AuthResultDto, ChangePasswordRes>();
        }
    }
}
