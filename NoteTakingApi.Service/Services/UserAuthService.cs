using Microsoft.Extensions.Options;
using NoteTakingApi.Common.Exceptions;
using NoteTakingApi.Common.Hellpers;
using NoteTakingApi.Common.Models;
using NoteTakingApi.DataAccess.Repositories;
using NoteTakingApi.Service.Dtos;
using System.Net;
using System.Threading.Tasks;

namespace NoteTakingApi.Service.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserAuthService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetByUsername(request.Username);

            if (user == null)
                throw new UnauthorizedException(new ErrorResponse("Username or Password is wrong."));

            var isCredentialsValid = EncryptionHelper.ValidateEncryptedPassword(user.Password, request.Password, _appSettings.SaltHC);

            if (!isCredentialsValid)
                throw new UnauthorizedException(new ErrorResponse("Username or Password is wrong."));

            var tokenHelper = new TokenHelper(_appSettings);

            var token = await tokenHelper.GenerateToken(user.Id, 7 * 24); // 7 days

            return new LoginResponseDto()
            {
                Id = user.Id,
                Username = user.Username,
                Token = token
            };
        }

        public async Task<RegisterResponseDto> Register(RegisterRequestDto request)
        {
            if (request == null)
                throw new ValidationException(new ErrorResponse("Request is null"));

            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password)
                || string.IsNullOrEmpty(request.RetypePassword))
                throw new ValidationException(new ErrorResponse("Username or Password or Retype Password is empty"));

            var validateRetypePassword = request.Password == request.RetypePassword;

            if (!validateRetypePassword)
                throw new ValidationException(new ErrorResponse("Password and Retype Password not match"));

            var user = await _userRepository.GetByUsername(request.Username);

            if(user != null)
                throw new ValidationException(new ErrorResponse("Username not exist"));

            var data = await _userRepository.Insert(new DataAccess.Entities.User()
            {
                Username = request.Username,
                Password = EncryptionHelper.EncryptPassword(request.Password, _appSettings.SaltHC)
            });

            return new RegisterResponseDto(data.Username, data.Id);
        }
    }
}
