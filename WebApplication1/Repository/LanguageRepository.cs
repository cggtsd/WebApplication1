using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public class LanguageRepository(BookStoreContext context)
    {
        private readonly BookStoreContext _context=context;


        public async Task<List<LanguageModel>> GetLanguages()
        {
            return await  _context.Language.Select(x => new LanguageModel()
            {
                Id= x.Id,
                Name = x.Name,
                Description = x.Description,
            }).ToListAsync();
        }
    }
}
