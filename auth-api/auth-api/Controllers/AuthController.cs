using Auth.Common.Constants;
using Auth.Common.Contracts;
using Auth.Common.Models.Request;
using Auth.Common.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.AccessTokenApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public IActionResult Login(LoginRequest loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("invalid-login-model");
            }

            if (!(loginModel.UserName == CommonConstants.UserNm && 
                loginModel.Password == CommonConstants.Pswd))
            {
                return Unauthorized();
            }

            var claims = new List<Claim>
            {
                new Claim(CommonConstants.ClimeTokenType,CommonConstants.ClimeTypeAccessToken),
                new Claim(ClaimTypes.Name,CommonConstants.UserNm),
                new Claim(ClaimTypes.Role,CommonConstants.UserRole)
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);

            #region set result data

            var data = new LoginResponse
            {
                data = new Data
                {
                    token = new Token
                    {
                        accessToken = accessToken,
                        pinToken = null,
                        refreshToken = null,
                        expires = 0
                    },
                    userData = new UserData
                    {
                        customerIdentity = null,
                        customerName = null,
                        customerNo = null,
                        customerPhone = "51****15",
                        userName = null,
                    }
                    
                },
                errorCode="0",
                errorMessage=""
            };

            #endregion

            return Ok(data);
        }

        [HttpPost]
        [Route("otp")]
        [Authorize]
        public IActionResult Otp(OtpRequest request)
        {
            var claims = new List<Claim>
            {
                new Claim(CommonConstants.ClimeTokenType,CommonConstants.ClimeTypeAccessToken),
                new Claim(ClaimTypes.Name,CommonConstants.UserNm),
                new Claim(ClaimTypes.Role,CommonConstants.UserRole)
            };

            string accessToken = _tokenService.GenerateAccessToken(claims);

            var claimsRefreshToken = new List<Claim>
            {
                new Claim(CommonConstants.ClimeTokenType,CommonConstants.ClimeTypeRefreshToken),
                new Claim(ClaimTypes.Name,CommonConstants.UserNm),
                new Claim(ClaimTypes.Role,CommonConstants.UserRole)
            };

            string refreshToken = _tokenService.GenerateRefreshToken(claimsRefreshToken);

            var response = new OtpResponse
            {
                Data = new DataOtp
                {
                    Token=new TokenOtp
                    {
                        AccessToken=accessToken,
                        PinToken="",
                        RefreshToken=refreshToken,
                        Expires=60
                    },
                    UserData=new UserDataOtp
                    {
                        CustomerIdentity= "100000145",
                        CustomerName="UGUR CEBECI",
                        CustomerNo= "ucebeci",
                        CustomerPhone="",
                        UserName=""
                    }
                },
                ErrorCode = "0",
                ErrorMessage = ""
            };

            return Ok(response);
        }


        [HttpGet]
        [Route("accounts")]
        [Authorize]
        public IActionResult Accounts()
        {
            string jsonStr =
            #region account info
                @"{
    ""errorCode"": ""0"",
    ""errorMessage"": """",
    ""data"": {
        ""accounts"": [
            {
                ""branch"": ""1100"",
                ""accountNumber"": ""115389"",
                ""suffix"": ""351"",
                ""iban"": ""AZ20CAPN00000000011538900001"",
                ""accountName"": ""TEST13D TEST15D"",
                ""accountType"": ""VDSZMVD"",
                ""balance"": 267.15,
                ""currency"": ""AZN""
            },
            {
                ""branch"": ""1100"",
                ""accountNumber"": ""115389"",
                ""suffix"": ""352"",
                ""iban"": ""AZ90CAPN00000000011538900002"",
                ""accountName"": ""MANAT HESABI"",
                ""accountType"": ""VDSZMVD"",
                ""balance"": 0.00,
                ""currency"": ""AZN""
            },
            {
                ""branch"": ""1100"",
                ""accountNumber"": ""115389"",
                ""suffix"": ""353"",
                ""iban"": ""AZ63CAPN00000000011538900003"",
                ""accountName"": ""TEST13D TEST15D"",
                ""accountType"": ""VDSZMVD"",
                ""balance"": 0.00,
                ""currency"": ""USD""
            },
            {
                ""branch"": ""1100"",
                ""accountNumber"": ""115389"",
                ""suffix"": ""354"",
                ""iban"": ""AZ36CAPN00000000011538900004"",
                ""accountName"": ""TEST13D TEST15D"",
                ""accountType"": ""VDSZMVD"",
                ""balance"": 0.00,
                ""currency"": ""EUR""
            }
        ]
    }
}";
            #endregion


            return Content(jsonStr, "application/json");
        }
    }
}
