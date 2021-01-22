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
            if (model.UserName != null)
            {
                var userlogin = await userService.SignInUser(model.UserName, model.Password, "true");
                if (userlogin != null)
                {                   
                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                        var token = new JwtSecurityToken(
                            issuer: _configuration["JWT:ValidIssuer"],
                            audience: _configuration["JWT:ValidAudience"],
                            expires: DateTime.Now.AddHours(3),
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                    return Unauthorized();
                }
            return Ok();
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