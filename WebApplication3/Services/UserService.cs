using AutoMapper;
using WebApplication3.DTOs;
using WebApplication3.Models;
using WebApplication3.Repositories;

namespace WebApplication3.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ApiResponse<RegistrationResponseDto>> RegisterUser(RegistrationRequestDto registrationRequestDto)
        {
            if (await _userRepository.isEmailExist(registrationRequestDto.Email))
            {
                return new ApiResponse<RegistrationResponseDto>
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = "Email already exists",
                    Data = null
                };
            }
            User user = _mapper.Map<User>(registrationRequestDto);
            var result =  await _userRepository.addUser(user);
            return new ApiResponse<RegistrationResponseDto>
            {
                StatusCode = StatusCodes.Status201Created,
                Message = "User registered successfully",
                Data = _mapper.Map<RegistrationResponseDto>(result)
            };
        }
    }
}
