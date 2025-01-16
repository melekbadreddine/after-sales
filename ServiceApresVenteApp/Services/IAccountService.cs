using Microsoft.AspNetCore.Identity;
using ServiceApresVente.Models;
using ServiceApresVenteApp.ViewModels;

namespace ServiceApresVenteApp.Services
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterViewModel model);
        Task AssignRoleAsync(User user);
        Task<bool> SignInUserAsync(LoginViewModel model);
        Task SignOutUserAsync();
        Task<User?> GetUserByEmailAsync(string email);
    }
}
