using E_Library.DTO;
using E_Library.Models;
using Microsoft.AspNetCore.Identity;

namespace E_Library.Services.Interface
{
    public interface IAuthenticateServices
    {
        Task<ResponseModel<User>> ValidateUser(LoginDTO model);
        Task<IdentityResult> RegisterUser(RegisterDTO registerModel);
        Task<ResponseModel<User>> GetUserProfile(string userId);
    }
}
