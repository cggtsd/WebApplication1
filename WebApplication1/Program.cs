using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplication1.Data;
using WebApplication1.Repository;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<BookStoreContext>(options=>options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStore;Integrated Security=true;"));
            builder.Services.AddControllersWithViews();
#if DEBUG
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
                //.AddViewOptions(options=>options.HtmlHelperOptions.ClientValidationEnabled=false);
#endif
            builder.Services.AddScoped<BookRepository,BookRepository>();
            builder.Services.AddScoped<LanguageRepository,LanguageRepository>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
