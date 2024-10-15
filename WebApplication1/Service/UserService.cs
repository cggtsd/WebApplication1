using System.Security.Claims;

namespace WebApplication1.Service
{
    public class UserService(IHttpContextAccessor context) : IUserService
    {
        private readonly IHttpContextAccessor _context = context;

        public string? GetUserId()
        {
            return _context.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public bool IsAuthenticated()
        {
            return _context.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
