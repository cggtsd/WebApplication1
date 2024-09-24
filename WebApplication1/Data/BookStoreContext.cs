using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class BookStoreContext:DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options):base(options) 
        {
            
        }

        public DbSet<Books> Books { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookStore;Integrated Security=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
