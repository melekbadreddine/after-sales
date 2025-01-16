using ServiceApresVente.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceApresVenteApp.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<Article?> GetArticleByIdAsync(int id);
        Task<bool> CreateArticleAsync(Article article);
        Task<bool> UpdateArticleAsync(Article article);
        Task<bool> DeleteArticleAsync(int id);
        SelectList GetClientsForDropdown();
        bool ArticleExists(int id);
    }
}
