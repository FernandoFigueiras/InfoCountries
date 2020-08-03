using CoreMVCStudy.Web.Data.Entities;
using CoreMVCStudy.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace CoreMVCStudy.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserHelper(
            UserManager<User> userManager,
            SignInManager<User> signInManager)//Injecta-se o UserManager porque estamos a fazer o bypass a esta classe
        {
            _userManager = userManager;//class do Core que faz a gestao dos utilizadores (CRiar, apagar, se existe?) 
            _signInManager = signInManager;//class do Core que tem as APi que faz login e logout
        }


        public async Task<IdentityResult> AddUserAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false);//este false e para nao deixar bloquear ao fim de 4 tentativas
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
