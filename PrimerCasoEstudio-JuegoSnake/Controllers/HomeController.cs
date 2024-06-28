using Microsoft.AspNetCore.Mvc;
using PrimerCasoEstudio_JuegoSnake.Models;
using System.Diagnostics;

namespace PrimerCasoEstudio_JuegoSnake.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SubmitForm(User usuario, IFormFile photo)
        {
            if (usuario != null)
            {
                if (photo != null && photo.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photo.CopyToAsync(memoryStream);
                        usuario.photo = memoryStream.ToArray();
                    }
                }

                using (var context = new DemoContext())
                {
                    context.Add(usuario);
                    await context.SaveChangesAsync();
                    return View("Index");
                }
            }
            return Content("<a>SALIO MAL</a>");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
