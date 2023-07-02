using DotNetMath3.API.DbContexts;
using DotNetMath3.API.ReqRes;
using DotNetMath3.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetMath3.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MathDataContext _dataContext;
        private readonly TokenService _tokenService;

        public AuthController(
            UserManager<IdentityUser> userManager,
            MathDataContext dataContext,
            TokenService tokenService)
        {
            _userManager = userManager;
            _dataContext = dataContext;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("api/Auth/Register")]
        public async Task<IActionResult> RegisterAsync(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.CreateAsync(
                new IdentityUser
                {
                    UserName = request.Email,
                    Email = request.Email,
                },
                request.Password
            );

            if (result.Succeeded)
            {
                request.Password = "";
                return CreatedAtAction(nameof(RegisterAsync), new { request.Email }, request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("api/Auth/Login")]
        public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email);
            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userInDb = _dataContext.Users.FirstOrDefault(user => user.Email == request.Email);
            if (userInDb is null)
            {
                return Unauthorized();
            }

            var accessToken = _tokenService.CreateToken(userInDb);
            await _dataContext.SaveChangesAsync();

            return Ok(new AuthResponse()
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = accessToken,
            });
        }
    }
}