using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;

namespace ServiceApresVenteApp.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ArticleService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.Include(a => a.User).ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            return await _context.Articles
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> CreateArticleAsync(Article article)
        {
            _context.Add(article);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateArticleAsync(Article article)
        {
            _context.Update(article);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public SelectList GetClientsForDropdown()
        {
            var clients = _userManager.GetUsersInRoleAsync("Client").Result;
            return new SelectList(clients, "Id", "Id");
        }

        public bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
