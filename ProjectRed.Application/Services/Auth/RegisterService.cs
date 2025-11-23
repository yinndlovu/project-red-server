using ProjectRed.Application.Validators;
using ProjectRed.Core.DTOs.Data;
using ProjectRed.Core.DTOs.Requests.Auth;
using ProjectRed.Core.DTOs.Responses;
using ProjectRed.Core.Entities;
using ProjectRed.Core.Exceptions;
using ProjectRed.Core.Interfaces.Repositories;
using ProjectRed.Core.Interfaces.Services.Auth;
using ProjectRed.Core.Interfaces.Services.Validators;

namespace ProjectRed.Application.Services.Auth
{
    public class RegisterService(IUserRepository userRepository, IPasswordHasher passwordHasher,
        IPasswordValidator passwordValidator) : IRegisterService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IPasswordValidator _passwordValidator = passwordValidator;

        public async Task<AuthResponse<UserDto>> RegisterAsync(RegisterRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                throw new InvalidInputException("An email is required");
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                throw new InvalidInputException("Name is required");
            }

            if (string.IsNullOrEmpty(request.Password))
            {
                throw new InvalidInputException("Password is required");
            }

            var (IsValid, Message) = YearValidator.ValidateYear(request.BirthYear);
            if (!IsValid)
            {
                return new AuthResponse<UserDto>
                {
                    Success = IsValid,
                    Message = Message
                };
            }

            var userExists = await _userRepository.UserEmailExists(request.Email);
            if (userExists)
            {
                throw new AlreadyExistsException("Email already exists");
            }

            bool isValidPassword = _passwordValidator.IsValid(request.Password);
            if (!isValidPassword)
            {
                return new AuthResponse<UserDto>
                {
                    Success = false,
                    Message = "Password is invalid"
                };
            }

            var hashedPassword = _passwordHasher.HashPassword(request.Password);

            var user = new User
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = hashedPassword,
                BirthYear = request.BirthYear,
                CountryCode = "ZA"
            };

            await _userRepository.AddAsync(user);
            bool added = await _userRepository.SaveChangesAsync();
            if (!added)
            {
                return new AuthResponse<UserDto>
                {
                    Success = false,
                    Message = "No user has been added"
                };
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };

            return new AuthResponse<UserDto>
            {
                Success = true,
                Message = "Successfully registered",
                Token = "token",
                Details = userDto
            };
        }
    }
}
