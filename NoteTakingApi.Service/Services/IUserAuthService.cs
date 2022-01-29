using NoteTakingApi.Service.Dtos;
using System.Threading.Tasks;

namespace NoteTakingApi.Service.Services
{
    public interface IUserAuthService
    {
        Task<Dtos.LoginResponseDto> Login(Dtos.LoginRequestDto request);
        Task<RegisterResponseDto> Register(RegisterRequestDto request);
    }
}
