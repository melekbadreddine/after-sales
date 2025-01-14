using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceApresVenteApp.ViewModels;
using ServiceApresVente.Models;
using System.Diagnostics;

namespace ServiceApresVenteApp.Controllers
{
    public class AccountController : Controller
    {
        // GET: AccountController
        private readonly UserManager<Client> userManager;
        private readonly SignInManager<Client> signInManager;
        public AccountController(UserManager<Client> userManager, SignInManager<Client> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Copy data from RegisterViewModel to IdentityUser
                var user = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    TelNumber = model.TelNumber,
                    UserName = model.FirstName,
                    Password = model.Password,
                    Email = model.Email

                };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
   
                    return RedirectToAction("index", "home");
                }

                // If there are any errors, add them to the ModelState object
                // which will be displayed by the validation summary tag helper
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Debug.WriteLine(error.Description);
                }
            }
            else
            {
                Debug.WriteLine(ModelState.IsValid);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Client");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }
    }
}