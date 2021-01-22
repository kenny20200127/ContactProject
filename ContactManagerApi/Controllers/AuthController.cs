using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ContactManagerApi.IService;
using ContactManagerApi.Models;
using AspnetIdentityCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ContactManagerApi.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserService userService, IConfiguration _configuration)
        {
            this.userService = userService;
            this._configuration = _configuration;
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthModel model)
        {
            IActionResult response = Unauthorized();
            if (model.UserName != null)
            {
                var userlogin = await userService.SignInUser(model.UserName, model.Password, "true");
                if (userlogin != null)
                {
                  var tokenString=  GenerateJSONWebToken(model);
                    response = Ok(new { token = tokenString });
                }
            }
            return response;
            }

        private string GenerateJSONWebToken(AuthModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.UserName),
                new Claim("DateOfJoing", DateTime.Now.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterUserAsync(model);

                if (result.isSuccess)
                    return Ok(result);


                return BadRequest(result);

            }

            return BadRequest("some property are not valid");
        }
    }
}