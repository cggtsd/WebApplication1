using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    public class AccountController(IAccountRepository accountRepository) : Controller
    {
        private readonly IAccountRepository _accountRepository = accountRepository;

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                //logic
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(userModel);
                }
                ModelState.Clear();
                //return View();
                return RedirectToAction("ConfirmEmail", new { email = userModel.Email });
            }
            //return View(userModel);
            return View();
        }
        [Route("login")]
        public IActionResult Login()
        {
            return View();

        }
        [HttpPost("login")]
        //public async Task<IActionResult> Login(SigninModel signinModel)
        public async Task<IActionResult> Login(SigninModel signinModel,string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordAsync(signinModel);
                if (result.Succeeded)
                {
                    returnUrl = returnUrl=="/reset-password" ? "" : returnUrl;
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                //ModelState.AddModelError("", "Invalid Credentials");
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to login");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked. Try after sometime");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View(signinModel);

        }
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Route("change-password")]
        public IActionResult ChangePassword()
        {
            return View();
        } 
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                //write code here
                var result = await _accountRepository.ChangePasswordAsync(changePasswordModel);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(changePasswordModel);
        }
        [HttpGet("confirm-email")]
        //public async Task<IActionResult> ConfirmEmail(string uid, string token)
        public async Task<IActionResult> ConfirmEmail(string uid,string token,string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };
            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                //write code here
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                    //ViewBag.IsSuccess = true;
                    model.EmailVerified = true;
                }

            }
            //return View();
                return View(model);
            }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    //model.IsConfirmed = true;
                    model.EmailVerified=true;
                    return View(model);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong.");
            }
            return View(model);
        }
        [AllowAnonymous,HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        } 
        [AllowAnonymous,HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                //code here
                var user = await _accountRepository.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _accountRepository.GenerateForgotPasswordTokenAsync(user);
                }
                ModelState.Clear();
                model.EmailSent = true;
            }
            return View(model);
        }
        [AllowAnonymous, HttpGet("reset-password")]
        public IActionResult ResetPassword(string uid,string token)
        {
            ResetPasswordModel model = new ResetPasswordModel
            {
                Token = token,
                UserId = uid

            };
            return View(model);
        }

        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result =await _accountRepository.ResetPasswordAsync(model);
                //code here
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);

                }
               foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
    }
}
