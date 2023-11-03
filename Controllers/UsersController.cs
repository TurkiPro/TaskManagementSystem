using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Requests;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Controllers
{
    public class UsersController : BaseApiController
    {

        #region Variables
        private readonly IUserService userService;
        #endregion

        #region Constructor
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        #endregion

        #region Methods

        /// <summary>
        /// This method used for user login and retrun access token
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Email) || string.IsNullOrEmpty(loginRequest.Password))
            {
                return BadRequest(new TokenResponse
                {
                    Error = "Missing login details",
                    ErrorCode = "L01"
                });
            }

            var loginResponse = await userService.LoginAsync(loginRequest);

            if (!loginResponse.Success)
            {
                return Unauthorized(new
                {
                    loginResponse.ErrorCode,
                    loginResponse.Error
                });
            }

            return Ok(loginResponse);
        }

        /// <summary>
        /// This method used for register new user
        /// </summary>
        /// <param name="signupRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Signup(SignupRequest signupRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    return BadRequest(new TokenResponse
                    {
                        Error = $"{string.Join(",", errors)}",
                        ErrorCode = "S01"
                    });
                }
            }

            var signupResponse = await userService.SignupAsync(signupRequest);

            if (!signupResponse.Success)
            {
                return UnprocessableEntity(signupResponse);
            }

            return Ok(signupResponse.Email);
        }

        /// <summary>
        /// This method used for check login user information
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("info")]
        public async Task<IActionResult> Info()
        {
            var userResponse = await userService.GetInfoAsync(UserID);

            if (!userResponse.Success)
            {
                return UnprocessableEntity(userResponse);
            }

            return Ok(userResponse);

        }

        #endregion



    }
}
