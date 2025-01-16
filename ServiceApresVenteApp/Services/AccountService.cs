using Microsoft.AspNetCore.Identity;
using ServiceApresVente.Models;
using ServiceApresVenteApp.ViewModels;

namespace ServiceApresVenteApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(RegisterViewModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Password = model.Password,
                Adress = model.Adress,
                TelNumber = model.TelNumber,
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public async Task AssignRoleAsync(User user)
        {
            var role = user.Id == 1 ? "Responsable" : "Client";
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> SignInUserAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
