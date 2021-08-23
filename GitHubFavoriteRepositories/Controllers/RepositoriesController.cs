using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Octokit;
using GitHubFavoriteRepositories.DbContexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubFavoriteRepositories.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly GitHubClient _client;
        private readonly GitHubIntegrationContext _context;
        private readonly string _login;

        public RepositoriesController(GitHubClient client, GitHubIntegrationContext context, IConfiguration configuration)
        {
            _client = client;
            _context = context;
            _login = configuration.GetValue<dynamic>("GitHubUserLogin");
        }

        public async Task<IActionResult> Index(string search)
        {
            IEnumerable<Repository> repositories = await _client.Repository.GetAllForUser(_login);

            if (!string.IsNullOrEmpty(search))
                repositories = repositories.Where(r => r.Name.Contains(search));

            return View(repositories);
        }

        public async Task<IActionResult> Details(long id)
        {
            var repository = await _client.Repository.Get(id);
            var favorite = await _context.FavoriteRepositories.FirstOrDefaultAsync(f => f.RepositoryId == id);

            if (favorite != null)
                ViewBag.FavoriteRepository = favorite;

            return View(repository);
        }

        public async Task<IActionResult> Favorites()
        {
            var repositories = await _client.Repository.GetAllForUser(_login);
            var favorites = await _context.FavoriteRepositories.ToListAsync();
            return View(repositories.Where(r => favorites.Any(f => f.RepositoryId == r.Id)));
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(FavoriteRepository favorite)
        {
            await _context.FavoriteRepositories.AddAsync(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Favorites));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(FavoriteRepository favorite)
        {
            _context.FavoriteRepositories.Remove(favorite);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Favorites));
        }
    }
}
