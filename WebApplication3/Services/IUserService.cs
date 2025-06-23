using WebApplication3.DTOs;

namespace WebApplication3.Services
{
    public interface IUserService
    {
        public  Task<ApiResponse<RegistrationResponseDto>> RegisterUser(RegistrationRequestDto registrationRequestDto);
        public Task<ApiResponse<String>> Login(LoginRequestDto loginRequestDto);
    }
}
