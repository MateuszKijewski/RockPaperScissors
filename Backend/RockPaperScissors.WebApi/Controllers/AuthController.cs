using Dawn;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissors.Application.Common.Interfaces;
using RockPaperScissors.Domain.Dtos.Auth;

namespace RockPaperScissors.WebApi.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = Guard.Argument(userService, nameof(userService)).NotNull().Value;
        }

        [HttpPost]
        [Route("api/[controller]/Register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterNewUser(registerDto);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var jwt = await _userService.HandleLogin(loginDto);

                return Ok(jwt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("api/[controller]/GetCurrentUser")]
        public ActionResult GetCurrentUser()
        {
            try
            {
                var user = _userService.GetCurrentUser();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}