using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    //public class AccountRepository(UserManager<IdentityUser> userManager) : IAccountRepository
    public class AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signinManager) : IAccountRepository
    {
        //private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signinManager = signinManager;

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {

            //var user = new IdentityUser()
            //{
            //    Email = userModel.Email,
            //    UserName = userModel.Email
            //};
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };
            var result= await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }
        public async Task<SignInResult> PasswordAsync(SigninModel signinModel)
        {
            var result = await _signinManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, true, false);
            return result;

         }

        public async Task SignOutAsync()
        {
            await _signinManager.SignOutAsync();
         }
        }
    }
