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

        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }


        public async Task<List<User>> findUserByQuery(FilterDto filterDto)
        {
            return await _userRepository.findUserByQuery(filterDto);
        }

        public async Task<ApiResponse<string>> Login(LoginRequestDto loginRequestDto)
        {
            User user = await _userRepository.findUserByEmail(loginRequestDto.email);
            if (user==null)
            {
                return new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "Email doesn't Exists",
                    Data = null
                };
            }
            if (user.password.Equals(loginRequestDto.password))
            {
                return new ApiResponse<string>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Authentication Done Successfully",
                    Data = null
                };
            }
            return new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Incorrect Password",
                Data = null
            };
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
            registrationRequestDto.age = CalculateAge(registrationRequestDto.DateOfBirth);
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
