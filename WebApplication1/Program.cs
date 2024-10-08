using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            IConfiguration configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddDbContext<BookStoreContext>(options =>options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStore;Integrated Security=true;"));
            //builder.Services.AddDbContext<BookStoreContext>(options =>options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();
            //builder.Services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 5;
            //    options.Password.RequiredUniqueChars = 1;
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;


            //});
            builder.Services.AddControllersWithViews();
#if DEBUG
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation()
                .AddViewOptions(static options => options.HtmlHelperOptions.ClientValidationEnabled = false);
#endif
            builder.Services.AddScoped<IBookRepository,BookRepository>();
            builder.Services.AddScoped<ILanguageRepository,LanguageRepository>();
            builder.Services.AddScoped<IAccountRepository,AccountRepository>();

            builder.Services.AddSingleton<IMessageRepository,MessageRepository>();

            builder.Services.Configure<NewBookAlertConfig>(configuration.GetSection("NewBookAlert"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
          
            app.MapControllers();

            app.Run();
        }
    }
}
