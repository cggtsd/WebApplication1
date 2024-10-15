using WebApplication1.Models;

namespace WebApplication1.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions emailOptions);
        Task SendEmailForConfirmation(UserEmailOptions emailOptions);
        Task SendEmailForForgotPassword(UserEmailOptions emailOptions);
    }
}