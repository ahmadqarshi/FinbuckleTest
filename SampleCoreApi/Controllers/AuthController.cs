using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SampleCoreApi.Context;
using SampleCoreApi.Entities;
using SampleCoreApi.Helpers;

namespace SampleCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class AuthController : ControllerBase
    {
        private readonly SchoolContext _dbContext;
        private readonly UserManager<MyAppUser> _userManager;

        public AuthController(UserManager<MyAppUser> userManager, SchoolContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public async Task<ApiResponse> Authenticate([FromBody] LoginUserRequest model)
        {
            if (!ModelState.IsValid) throw new ApiException("Invalid parameters", 400);

            try
            {
                var user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);

                if (user == null)
                {
                    //Email is incorrect.
                    throw new ApiException("Invalid Credentials", 400);
                }


                var passwordIsValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordIsValid)
                {
                    //Password is incorrect.
                    throw new ApiException("Invalid Credentials", 400);
                }
                else
                {
                    return new ApiResponse(new { result = "Successful" });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }

    public class LoginUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
