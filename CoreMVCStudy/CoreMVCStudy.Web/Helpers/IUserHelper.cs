using CoreMVCStudy.Web.Data.Entities;
using CoreMVCStudy.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CoreMVCStudy.Web.Helpers
{
    public interface IUserHelper
    {
        Task<IdentityResult> AddUserAsync(User user, string password); //Este metodo faz o bypass do create async da classe do usermanager


        Task<User> GetUserByEmailAsync(string email);


        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();
    }
}
