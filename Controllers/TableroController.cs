using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;
using tl2_tp10_2023_IvanDMir.ViewModels;
namespace tl2_tp10_2023_IvanDMir.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    TableroRepositorios repo;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        repo = new TableroRepositorios();
    }

      [HttpGet]
    public IActionResult Index() {
        if(!(isLogin())) return RedirectToRoute(new { controller = "Login", action = "Index"});
        if(esAdmin()) return View(new GBViewModel(repo.GetAll()));
        var loggedUserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
        return View(new GBViewModel(repo.GetByUser(loggedUserId)));
    }

    [HttpGet]
    public IActionResult Add() => View(new ABViewModel());

    [HttpPost]
    public IActionResult Add(ABViewModel tablero) {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var newTablero = new Tablero() {
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario
        };
        repo.Crear(newTablero);
        return RedirectToAction("Index");
    }
     [HttpGet]
    public IActionResult Update(int id) => View(new UBViewModel(repo.GetById(id)));

    [HttpPost]
    public IActionResult Update(UBViewModel tablero) {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var TableroViejo = repo.GetAll().FirstOrDefault(b => b.IdTablero == tablero.Id);
        repo.Update(TableroViejo.IdTablero, TableroViejo);
        return RedirectToAction("Index");
    }
     [HttpGet]
    public IActionResult Delete(int id) {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        repo.Delete(id);
        return RedirectToAction("Index");
    }

      private bool isLogin()
    {
        if (HttpContext.Session != null ){
            return true;
        }else{
            return false;
        }
    }

    private bool esAdmin(){
         if (HttpContext.Session.GetString("Rol") == Enum.GetName(Roles.admin)){
            return true;
         }
         var colo = HttpContext.Session.GetString("Rol");
         var malo =  Enum.GetName(Roles.admin);
         return false;
        
    }
}