using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceApresVente.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceApresVente.Controllers
{
    public class ResponsableController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsableController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string motDePasse)
        {
            // Vérification des informations du responsable dans la base
            var responsable = await _context.Responsables
                .FirstOrDefaultAsync(r => r.Email == email && r.MotDePasse == motDePasse);

            if (responsable != null)
            {
                // Transmettre le responsable au Dashboard via un modèle ou ViewBag
                return RedirectToAction("Dashboard", new { id = responsable.Id });
            }

            // Si la connexion échoue
            ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect.");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard(int id)
        {
            // Récupérer le responsable depuis la base
            var responsable = await _context.Responsables.FindAsync(id);
            if (responsable == null)
            {
                return RedirectToAction("Login");
            }

            // Passer les informations à la vue
            ViewBag.Responsable = responsable;
            return View();
        }





        // Déconnexion de l'utilisateur
        [HttpPost]
        public IActionResult Logout()
        {
            // Redirige simplement vers la page de connexion
            return RedirectToAction("Login");
        }

    }

}
