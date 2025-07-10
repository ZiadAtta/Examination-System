using Examination_System.Common;
using Examination_System.DTOS;
using Examination_System.Models;
using Examination_System.Repository;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;
        public AuthorizationController(IConfiguration config)
        {
            _authorizationService = new AuthorizationService(config);
        }
        [HttpPost("Register")]
        public async Task<GeneralResponse<string>> Register(RegisterDTO model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new GeneralResponse<string>
                {
                    Data = "There are Errors.",
                    Message = errors.ToString(),
                    ErrorCode = ErrorCode.InvalidInput.ToString(),
                    IsSuccess = false
                };
            }

            return await _authorizationService.Register(model);
        }

        [HttpPost("Login")]
        public async Task<GeneralResponse<string>> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new GeneralResponse<string>
                {
                    Data = "There are Errors.",
                    Message = errors.ToString(),
                    ErrorCode = ErrorCode.InvalidInput.ToString(),
                    IsSuccess = false
                };
            }

            return  await _authorizationService.Login(model);
        }
    }
}
