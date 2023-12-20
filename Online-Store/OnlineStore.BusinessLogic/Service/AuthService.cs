using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStore.BusinessLogic.Constants;
using OnlineStore.BusinessLogic.Dtos.Auth;
using OnlineStore.DataAccess.DbContexts;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.BusinessLogic.Services
{
    public interface IAuthService
    {
        Task<AuthResultDto> LoginUser(LoginDto loginDto);
        Task<AuthResultDto> RegisterUser(RegisterDto registerDto);
        Task<AuthResultDto> ChangePasswordUser(ChangePasswordDto changePasswordDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly OnlineStoreDbContext _context;

        public AuthService(IMapper mapper, OnlineStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AuthResultDto> LoginUser(LoginDto loginDto)
        {
            var result = new AuthResultDto();

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == loginDto.Email && x.Password == loginDto.Password);
            
            if (user == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundUser;
                return result;
            }

            result.Success = true;

            return result;
        }

        public async Task<AuthResultDto> RegisterUser(RegisterDto registerDto)
        {
            var result = new AuthResultDto();

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == registerDto.Email);

            if (user != null)
            {
                result.ErrorCode = ErrorCodes.AccountAlreadyExists;
                return result;
            }

            var userEntity = _mapper.Map<User>(registerDto);
            userEntity.Role = 3;

            var cartEntity = new Cart
            {
                Customer = userEntity
            };

            _context.Users.Add(userEntity);
            _context.Carts.Add(cartEntity);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }

        public async Task<AuthResultDto> ChangePasswordUser(ChangePasswordDto changePasswordDto)
        {
            var result = new AuthResultDto();

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == changePasswordDto.Email);

            if (user == null)
            {
                result.ErrorCode = ErrorCodes.IncorrectEmail;
                return result;
            }

            if (user.Password != changePasswordDto.PasswordOld)
            {
                result.ErrorCode = ErrorCodes.IncorrectPassword;
                return result;
            }

            user.Password = changePasswordDto.PasswordNew;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }
    }
}
