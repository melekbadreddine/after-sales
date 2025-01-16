using ServiceApresVente.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceApresVenteApp.Services
{
    public interface IClientService
    {
        Task<List<Article>> GetClientArticlesAsync(string userId);
        Task<User> GetClientByIdAsync(string userId);
    }
}
