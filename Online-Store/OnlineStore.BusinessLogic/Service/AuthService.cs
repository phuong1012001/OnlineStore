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
        Task<AuthResultDto> RegisterUser(UserDto registerDto);
        Task<AuthResultDto> ChangePasswordUser(ChangePasswordDto changePasswordDto);
        Task<List<UserDto>> GetUsers();
        Task<UserDto?> GetUser(int id);
        Task<List<UserDto>> GetSearch(string searchString);
        Task<AuthResultDto> SaveUser(UserDto userDto);
        Task<AuthResultDto> DeleteUser(int id);
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

        public async Task<AuthResultDto> RegisterUser(UserDto userDto)
        {
            var result = new AuthResultDto();

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == userDto.Email);

            if (user != null)
            {
                result.ErrorCode = ErrorCodes.AccountAlreadyExists;
                return result;
            }

            var userEntity = _mapper.Map<User>(userDto);

            if (userEntity.Role == 3)
            {
                var cartEntity = new Cart
                {
                    Customer = userEntity
                };

                _context.Carts.Add(cartEntity);
            }

            _context.Users.Add(userEntity);
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

        public async Task<List<UserDto>> GetUsers()
        {
            var result = new List<UserDto>();

            var users = _context.Users.ToList();

            result = _mapper.Map<List<UserDto>>(users);

            return result;
        }

        public async Task<UserDto?> GetUser(int id)
        {
            var result = new UserDto();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundUser;
                return result;
            }

            result = _mapper.Map<UserDto>(user);

            return result;
        }

        public async Task<List<UserDto>> GetSearch(string searchString)
        {
            var result = new List<UserDto>();

            var users = _context.Users
                .Where(s => (s.FristName + " " + s.LastName)!.Contains(searchString)
                            || s.Email!.Contains(searchString))
                .ToList();

            result = _mapper.Map<List<UserDto>>(users);

            return result;
        }

        public async Task<AuthResultDto> SaveUser(UserDto userDto)
        {
            var result = new AuthResultDto();

            var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == userDto.Id);

            if (user == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundUser;
                return result;
            }

            user.FristName = userDto.FristName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.PhoneNumber = userDto.PhoneNumber;
            user.Role = userDto.Role;
            user.Civilianld = userDto.Civilianld;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }

        public async Task<AuthResultDto> DeleteUser(int id)
        {
            var result = new AuthResultDto();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                result.ErrorCode = ErrorCodes.NotFoundUser;
                return result;
            }

            user.isDeleted = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            result.Success = true;
            return result;
        }
    }
}
