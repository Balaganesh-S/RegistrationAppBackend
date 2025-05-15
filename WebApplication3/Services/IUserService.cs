using WebApplication3.DTOs;

namespace WebApplication3.Services
{
    public interface IUserService
    {
        public  Task<ApiResponse<RegistrationResponseDto>> RegisterUser(RegistrationRequestDto registrationRequestDto);
    }
}
