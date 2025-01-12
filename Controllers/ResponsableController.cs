using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceApresVente.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ServiceApresVente.Controllers
{
    public class ResponsableController : Controller
    {
        // Liste simulée des responsables
        private static List<ResponsableSAV> responsables = new List<ResponsableSAV>
        {
            new ResponsableSAV { Id = 1, Nom = "Ala", Email = "ala@example.com", MotDePasse = "password123" },
            new ResponsableSAV { Id = 2, Nom = "Melek", Email = "melek@example.com", MotDePasse = "password123" }
        };

        // Afficher la page de connexion
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Traitement de la connexion
        [HttpPost]
        public async Task<IActionResult> Login(string email, string motDePasse)
        {
            // Recherche du responsable par email
            var responsable = responsables.Find(r => r.Email == email);

            // Vérification des informations d'identification
            if (responsable != null && responsable.MotDePasse == motDePasse)
            {
                // Création des revendications pour le cookie d'authentification
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, responsable.Nom),
                    new Claim(ClaimTypes.Email, responsable.Email),
                    new Claim("ResponsableId", responsable.Id.ToString())
                };

                // Créer un identité basée sur les revendications
                var claimsIdentity = new ClaimsIdentity(claims, "Login");

                // Créer un principal avec l'identité
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Se connecter en utilisant un cookie
                await HttpContext.SignInAsync("ResponsableSAV", claimsPrincipal);

                // Rediriger l'utilisateur vers la page d'accueil ou une page protégée
                return RedirectToAction("Index", "Home");
            }

            // Si la connexion échoue, retourner à la page de connexion avec un message d'erreur
            ModelState.AddModelError(string.Empty, "Nom d'utilisateur ou mot de passe incorrect.");
            return View();
        }

        // Déconnexion de l'utilisateur
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("ResponsableSAV");
            return RedirectToAction("Login");
        }
    }
}
