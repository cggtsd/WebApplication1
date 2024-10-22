using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Repository
{
    //public class AccountRepository(UserManager<IdentityUser> userManager) : IAccountRepository
    public class AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signinManager,IUserService userService,IEmailService emailService,IConfiguration configuration) : IAccountRepository
    {
        //private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly SignInManager<ApplicationUser> _signinManager = signinManager;
        private readonly IUserService _userService = userService;
        private readonly IEmailService _emailService = emailService;
        private readonly IConfiguration _configuration = configuration;

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
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
            if (result.Succeeded)
            {
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //if (!string.IsNullOrEmpty(token))
                //{
                //    await SendEmailConfirmationAsync(user, token);
                //}
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }
        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationAsync(user, token);
            }

        }
        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }

        }
        public async Task<SignInResult> PasswordAsync(SigninModel signinModel)
        {
            var result = await _signinManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, signinModel.RememberMe, false);
            return result;

         }

        public async Task SignOutAsync()
        {
            await _signinManager.SignOutAsync();
         }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var userId = _userService.GetUserId();
            var user= await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);

        }
        public async Task<IdentityResult> ConfirmEmailAsync(string uid,string token)
        {
          return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);

        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
          return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token,model.NewPassword);

        }
        private async Task SendEmailConfirmationAsync(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions options = new()
            {
                ToEmails = [user.Email],
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Username}}",user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain + confirmationLink,user.Id,token))
                }
            };
             await _emailService.SendEmailForConfirmation(options);

        }
        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;
            UserEmailOptions options = new()
            {
                ToEmails = [user.Email],
                Placeholders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{Username}}",user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appDomain+confirmationLink,user.Id,token))
                }
            };
             await _emailService.SendEmailForForgotPassword(options);

        }

    }
    }
