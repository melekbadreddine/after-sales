using Microsoft.AspNetCore.Identity;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApresVenteApp.Services
{
    public class ClientService : IClientService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public ClientService(UserManager<User> userManager, IUserRepository userRepository)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<List<Article>> GetClientArticlesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.Articles?.ToList() ?? new List<Article>();
        }

        public async Task<User> GetClientByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }
    }
}
