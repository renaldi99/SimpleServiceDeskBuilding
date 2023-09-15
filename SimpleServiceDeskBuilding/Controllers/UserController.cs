using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleServiceDeskBuilding.DTOs;
using SimpleServiceDeskBuilding.Exceptions;
using SimpleServiceDeskBuilding.Helpers;
using SimpleServiceDeskBuilding.Message;
using SimpleServiceDeskBuilding.Models;
using SimpleServiceDeskBuilding.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;

namespace SimpleServiceDeskBuilding.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public UserController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration ?? throw new ArgumentNullException();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult> RegisterUser([FromBody] UserReqDto user)
        {
            var checkIdentity = Constants.identity_employee.Contains(user.identity_employee);
            if (!checkIdentity)
            {
                throw new BadRequestException("Identity can only internal or vendor");
            }

            var userData = new UserModel
            {
                fullname = user.fullname,
                username = user.username,
                email = user.email,
                password = CommonUtils.EncryptPassword(user.password),
                identity_employee = user.identity_employee,
                is_active = "active",
                role = "user",
                refresh_token = ""
            };

            _ = await _unitOfWork.UserService.CreateNewUser(userData);
            return Ok(new DefaultMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success create user" });
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetUserById([FromBody] UserReqIdDto user)
        {
            var resDatauser = await _unitOfWork.UserService.GetUserById(user.user_id);

            return Ok(new PayloadMessage { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success get data user", payload = resDatauser });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<ActionResult> LoginUser([FromQuery] string username, string password)
        {
            if (username == "" && password == "")
            {
                throw new BadRequestException("Invalid request");
            }

            var checkUser = await _unitOfWork.UserService.GetUserByUsername(username);
            var checkPass = CommonUtils.DecryptPassword(checkUser.password);

            if (checkUser == null || !checkPass.Equals(password))
            {
                throw new BadRequestException("User account isn't registered");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, checkUser.username)
            };

            var _token = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler().CreateToken(_token);
            var tokenResult = new JwtSecurityTokenHandler().WriteToken(tokenHandler);

            return Ok(new { isSuccess = true, statusCode = StatusCodes.Status200OK, message = "Success login", token = tokenResult });
        }
    }
}
