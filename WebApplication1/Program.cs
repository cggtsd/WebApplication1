using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Helpers;
using WebApplication1.Models;
using WebApplication1.Repository;
using WebApplication1.Service;

namespace WebApplication1
{
    public class Program
    {
        
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration _configuration = builder.Configuration;

            // Add services to the container.
            //builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStore;Integrated Security=true;"));
            builder.Services.AddDbContext<BookStoreContext>(options =>options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>()
                                .AddDefaultTokenProviders();
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });
            builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(5);
            });
            builder.Services.ConfigureApplicationCookie(config => config.LoginPath = _configuration["Application:LoginPath"]);
            builder.Services.AddControllersWithViews();
#if DEBUG
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation()
                .AddViewOptions(static options => options.HtmlHelperOptions.ClientValidationEnabled = true);
#endif
            builder.Services.AddScoped<IBookRepository,BookRepository>();
            builder.Services.AddScoped<ILanguageRepository,LanguageRepository>();
            builder.Services.AddScoped<IAccountRepository,AccountRepository>();

            builder.Services.AddSingleton<IMessageRepository,MessageRepository>();
            builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();

            //builder.Services.Configure<NewBookAlertConfig>(_configuration.GetSection("NewBookAlert"));
            builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.Configure<SMTPConfigModel>(_configuration.GetSection("SMTPConfig"));
            builder.Services.Configure<NewBookAlertConfig>("ThirdPartyBook", _configuration.GetSection("ThirdPartyBook"));
            builder.Services.Configure<NewBookAlertConfig>("InternalBook", _configuration.GetSection("NewBookAlert"));

            //builder.Services.AddSession();
            builder.Services.AddSession(options=>options.IdleTimeout=TimeSpan.FromMinutes(5));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider=new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory() , "MyStaticFiles")),
                RequestPath="/MyStaticFiles"
            });

            app.UseRouting();
            //app.UseAuthentication();

            app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.MapControllerRoute(
            //    name: "AboutUs",
            //    pattern: "about-us/{id?}",
            //    defaults: new { controller = "Home", action = "AboutUs" });
          
            app.MapControllerRoute(
                name: "MyArea",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
          
            app.MapControllers();
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Admin", "Manager", "Member" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string email = "admin@admin.com";
                string password = "54321";
                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new ApplicationUser();
                    user.UserName = email;
                    user.Email = email;
                    user.EmailConfirmed = true;
                    await userManager.CreateAsync(user, password);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

           
            app.Run();
        }
    }
}
