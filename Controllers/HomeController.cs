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
        try{
            return View();
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
     public IActionResult IndexLogueado()
    {
        try{
            return View();
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    public IActionResult Desloguear()
    {
        HttpContext.SignOutAsync();
        return RedirectToRoute(new { controller = "Login", action = "Index"});
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
