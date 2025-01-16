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

        // Enregistrer un utilisateur et retourner un IdentityResult
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

            var result = await _userManager.CreateAsync(user, model.Password);
            return result; // Retourne le résultat de l'enregistrement
        }

        // Assigner un rôle en fonction de l'ID utilisateur
        public async Task AssignRoleAsync(User user)
        {
            var role = user.Id == 1 ? "Responsable" : "Client"; // Id == 1 est responsable, sinon client
            await _userManager.AddToRoleAsync(user, role);
        }

        // Connexion de l'utilisateur
        public async Task<bool> SignInUserAsync(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result.Succeeded;
        }

        // Déconnexion de l'utilisateur
        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        // Trouver un utilisateur par son email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        // Vérifier si un utilisateur appartient à un rôle
        public async Task<bool> IsUserInRoleAsync(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
