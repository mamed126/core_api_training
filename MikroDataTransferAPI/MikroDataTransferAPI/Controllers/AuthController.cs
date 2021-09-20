using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MikroDataTransferAPI.Contracts;
using MikroDataTransferAPI.Dto;
using MikroDataTransferAPI.Model;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MikroDataTransferAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private ILoggerManager _logger;
        private IConfiguration _configuration;
        private IRepositoryWrapper _repoWrapper;
        public AuthController(ILoggerManager logger, 
            IConfiguration configuration, 
            IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _configuration = configuration;
              _repoWrapper=repoWrapper;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody]UserloginDto userLoginDto)
        {
            try
            {
                var repo = _repoWrapper.AuthRepo;
                var user = repo.Login(userLoginDto.UserName,
                    userLoginDto.Password);

                if (user == null)
                {
                    return Unauthorized();
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.RoleName)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                        , SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(tokenString);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside login action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            var repo = _repoWrapper.AuthRepo;
            if (await repo.UserExists(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName,
                RoleName=userForRegisterDto.RoleName
            };

            var createdUser = await repo.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }
    }
}
