using E_Library.DTO;
using E_Library.Models;
using E_Library.Services.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace E_Library.Services
{
    public class AuthenticateServices : IAuthenticateServices
    {
        private readonly ElibraryContext _Context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthenticateServices(ElibraryContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _Context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResponseModel<User>> ValidateUser(LoginDTO model)
        {
            if (model == null)
                return new ResponseModel<User>
                {
                    IsSuccess = false,
                    Message = "Invalid Login Details"
                };
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return new ResponseModel<User>
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            if (!result.Succeeded)
            {
                return new ResponseModel<User>
                {
                    IsSuccess = false,
                    Message = "wrong password"
                };
            }
            return new ResponseModel<User>
            {
                IsSuccess = true,
                Message = "Login Successful",
                Data = user
            };
        }
        public async Task<IdentityResult> RegisterUser(RegisterDTO registerModel)
        {

            var newUser = new User
            {
                UserName = registerModel.Email,
                Name = registerModel.Name,
                UniversityId = registerModel.UniversityId,
                Role = registerModel.Role,
                Password = registerModel.Password
            };
            var result = await _userManager.CreateAsync(newUser, registerModel.Password);
            if(result.Succeeded && !string.IsNullOrEmpty(registerModel.Role))
            {
                await _userManager.AddToRoleAsync(newUser, registerModel.Role);
            }
            return result;
        }


        public async Task<ResponseModel<User>> GetUserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResponseModel<User>
                {
                    IsSuccess = false,
                    Message = "User not found."
                };
            }

            return new ResponseModel<User>
            {
                IsSuccess = true,
                Message = "User profile retrieved successfully.",
                Data = user
            };
        }

        //public Task<ResponseModel<User>> GetUserProfile(string userId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

