using Auth.Common.Constants;
using Auth.Common.Contracts;
using Auth.Common.Models;
using Auth.Common.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.RefreshTokenApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RefreshController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public RefreshController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentException(nameof(tokenService));
        }

        [HttpPost, Route("newtoken")]
        [Authorize]
        public IActionResult Refresh()
        {
            var claims = new List<Claim>
            {
                new Claim(CommonConstants.ClimeTokenType ,CommonConstants.ClimeTypeRefreshToken),
                new Claim(ClaimTypes.Name,CommonConstants.UserNm),
                new Claim(ClaimTypes.Role,CommonConstants.Pswd)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken(claims);

            var response = new RefreshResponse
            {
                Data = new DataOtp
                {
                    Token = new TokenOtp
                    {
                        AccessToken = accessToken,
                        PinToken = "",
                        RefreshToken = refreshToken,
                        Expires = 60
                    },
                    UserData = new UserDataOtp
                    {
                        CustomerIdentity = "100000145",
                        CustomerName = "UGUR CEBECI",
                        CustomerNo = "ucebeci",
                        CustomerPhone = "",
                        UserName = ""
                    }
                },
                ErrorCode = "0",
                ErrorMessage = ""
            };

            return Ok(response);
        }
    }
}
