using ContactManagerApi.IService;
using AspnetIdentityCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerApi.Services
{
    public class UserServices : IUserService
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        public UserServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        public async Task<UserManagerResponse> SignInUser(string UserName, string Password, string client)
        {
            var user = await userManager.FindByEmailAsync(UserName);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "invalid credential",
                    isSuccess = false,
                };
            }
            SignInResult result = null;
            if (client == "true")
            {
                result = await signInManager.CheckPasswordSignInAsync(user, Password, false);
            }
            else
            {

                result = await signInManager.PasswordSignInAsync(user, Password, true, false);
            }
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Operation successful",
                    isSuccess = true,
                };
            }
            return new UserManagerResponse
            {
                Message = "User or Password incorrect or inactive",
                isSuccess = false,
            };

        }
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Register model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password does not match the password",
                    isSuccess = false,
                };

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "user created successfull",
                    isSuccess = true
                };
            }

            return new UserManagerResponse
            {
                Message = "user did not create",
                isSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };     
        }
    }
}
