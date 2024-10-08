using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class AccountRepository(UserManager<IdentityUser> userManager) : IAccountRepository
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;

        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {

            var user = new IdentityUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
          var result= await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }
    }
}
