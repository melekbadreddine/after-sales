using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceApresVente.Models;
using ServiceApresVenteApp.Services;

namespace ServiceApresVenteApp.Controllers
{
    [Authorize(Roles = "Responsable")]
    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return View(articles);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var article = await _articleService.GetArticleByIdAsync(id.Value);
            if (article == null) return NotFound();

            return View(article);
        }

        public IActionResult Create()
        {
            ViewData["UserId"] = _articleService.GetClientsForDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Article article)
        {
            if (ModelState.IsValid)
            {
                var success = await _articleService.CreateArticleAsync(article);
                if (success) return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = _articleService.GetClientsForDropdown();
            return View(article);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var article = await _articleService.GetArticleByIdAsync(id.Value);
            if (article == null) return NotFound();

            ViewData["UserId"] = _articleService.GetClientsForDropdown();
            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Article article)
        {
            if (id != article.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _articleService.UpdateArticleAsync(article);
                if (success) return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"] = _articleService.GetClientsForDropdown();
            return View(article);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var article = await _articleService.GetArticleByIdAsync(id.Value);
            if (article == null) return NotFound();

            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _articleService.DeleteArticleAsync(id);
            if (success) return RedirectToAction(nameof(Index));

            return Problem("Unable to delete article.");
        }
    }
}
