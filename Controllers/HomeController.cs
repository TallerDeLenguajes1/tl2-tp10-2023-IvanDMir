using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_IvanDMir.Models;
using Microsoft.AspNetCore.Authentication;

namespace tl2_tp10_2023_IvanDMir.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
         if (HttpContext.User.Identity.IsAuthenticated){
             return RedirectToRoute(new { controller = "Home", action = "IndexLogueado"});
         }
        return View();
    }
     public IActionResult IndexLogueado()
    {
        if ((HttpContext.User.Identity.IsAuthenticated)){
             return RedirectToRoute(new { controller = "Home", action = "Index"});
         }
        
        return View();
    }

    public IActionResult Desloguear()
    {
        HttpContext.SignOutAsync();
        return RedirectToRoute(new { controller = "Home", action = "Index"});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
