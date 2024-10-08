using WebApplication1.Models;

namespace WebApplication1.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}