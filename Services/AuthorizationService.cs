using Examination_System.Common;
using Examination_System.DTOS;
using Examination_System.Models;
using Examination_System.Repository;
using Examination_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Services
{
    public class AuthorizationService
    {
        private readonly Repository<User> _userRepo;
        private readonly IConfiguration _config;
        public AuthorizationService( IConfiguration config)
        {
            _userRepo = new Repository<User>();
            _config = config;
        }

        public async Task<GeneralResponse<string>> Register(RegisterDTO model)
        {
            User user = new User
            {
                Username = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                Role = model.Role == "Instructor" ? UserRole.Instructor : UserRole.Student,
            };

            var userExists = await _userRepo.GetAsync(x => x.Username == model.UserName || x.Email == model.Email);
            if (userExists.Count() > 0)
            {
                return new GeneralResponse<string>
                {
                    Data = null,
                    Message = "User already exists.",
                    ErrorCode = ErrorCode.UserAlreadyExists.ToString(),
                    IsSuccess = false
                };
            }

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            var generate = new GenerateToken(_config);

            var token = generate.Generate(user.Id, user.Username, model.Role);

            return new GeneralResponse<string>
            {
                Data = token,
                Message = "User registered successfully.",
                IsSuccess = true
            };
        }
        public async Task<GeneralResponse<string>> Login(LoginVM model)
        {
            //Improve 
            var userExists = await _userRepo.GetAsync(x => x.Username == model.UserName || x.PasswordHash == model.Password);
            var user =await userExists.FirstOrDefaultAsync();
            if (user == null)
            {
                return new GeneralResponse<string>
                {
                    Data = null,
                    Message = "Error username or password.",
                    ErrorCode = ErrorCode.UserAlreadyExists.ToString(),
                    IsSuccess = false
                };
            }

            var generate = new GenerateToken(_config);
            var token = generate.Generate(user.Id, user.Username, user.Role.ToString());

            return new GeneralResponse<string>
            {
                Data = token,
                Message = "Login successfully.",
                IsSuccess = true
            };

        }
    }
}
