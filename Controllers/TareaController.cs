using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;
using tl2_tp10_2023_IvanDMir.ViewModels;

namespace tl2_tp10_2023_IvanDMir.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    ITareasRepositorio repo;

    public TareaController(ILogger<TareaController> logger, ITareasRepositorio tareasRepositorio)
    {
        _logger = logger;
        repo = tareasRepositorio;
    }

  [HttpGet]
    public IActionResult Index() {
        try { 
        
        if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
        
        if(esAdmin()) return View(new GTViewModel(repo.GetAll()));
        
        var loggedUserId = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                return View(new GTViewModel(repo.GetByUser(loggedUserId)));
        
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
 [HttpGet]
    public IActionResult Add(){

        try{ 
            if(esAdmin()) return View(new ATViewModel());
            return RedirectToRoute(new { controller = "Tarea", action = "Index" });
       }catch(Exception ex){
        _logger.LogError(ex.ToString());
        return BadRequest();
       }
    }

    [HttpPost]
    public IActionResult Add(ATViewModel tarea) {
        try { 
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var nuevaTarea = new Tarea() {
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion,
            Estado = Estados.ToDo,
            Color = tarea.Color,
            IdTablero = tarea.IdTablero
        };
        repo.Crear(nuevaTarea);
        
        return RedirectToAction("Index");
        }catch(Exception ex){
             _logger.LogError(ex.ToString());
             return BadRequest();
        }
    }
   [HttpGet]
    public IActionResult Update(int id) { 
        try{ 
            if(esAdmin()) return View(new UTViewModel(repo.GetById(id)));
            return RedirectToRoute(new { controller = "Tarea", action = "Index" });
        }catch(Exception ex){
             _logger.LogError(ex.ToString());
             return BadRequest();

        }
    }
    [HttpPost]
    public IActionResult Update(UTViewModel tarea) {
        try { 
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var nuevaTarea = new Tarea() {
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion,
            Estado = Estados.ToDo,
            Color = tarea.Color,
            IdTablero = tarea.IdTablero
        };
        repo.Update(tarea.Id, nuevaTarea);
        return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
             return BadRequest();
        }
    }


    [HttpGet]
    public IActionResult Delete(int id) {
        if(!esAdmin()) return RedirectToAction("Index");
        repo.Delete(id);
        return RedirectToAction("Index");
    }
       private bool isLogin()
    {
        if (HttpContext.Session.GetString("Rol") != null ){
            return true;
        }else{
            return false;
        }
    }

    private bool esAdmin(){
         if (HttpContext.Session.GetString("Rol") == Enum.GetName(Roles.admin)){
            return true;
         }
         return false;
        
    }
}