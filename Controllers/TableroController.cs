using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;
using tl2_tp10_2023_IvanDMir.ViewModels;
namespace tl2_tp10_2023_IvanDMir.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    ITableroRepositorio repo;

    public TableroController(ILogger<TableroController> logger,ITableroRepositorio tableroRepositorio)
    {
        _logger = logger;
        repo = tableroRepositorio;
    }

      [HttpGet]
    public IActionResult Index() {

             try{
            if(isLogin()){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }else if(esAdmin()){
                GBViewModel viewTableros = new GBViewModel(repo.GetAll());
                
                return View(viewTableros);
            }else{
                GBViewModel tableros = new GBViewModel(repo.GetAll().FindAll(t => t.IdUsuarioPropietario == HttpContext.Session.GetInt32("id")));
                return View(tableros);
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]
    public IActionResult Add(){
        try{ 
          if(esAdmin()){

           return View(new ABViewModel());
          }
           return RedirectToRoute(new{controller = "Login", action = "Index"});
        }
        catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
     }
    }

    [HttpPost]
    public IActionResult Add(ABViewModel tablero) {
        try {

        
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var newTablero = new Tablero() {
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario
        };
        repo.Crear(newTablero);
        return RedirectToAction("Index");
    }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
    }
    }

     [HttpGet]
    public IActionResult Update(int id) {
        try{ 
            if(esAdmin()){
                View(new UBViewModel(repo.GetById(id)));
            }
             return RedirectToRoute(new{controller = "Login", action = "Index"});
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }            


    } 


    [HttpPost]
    public IActionResult Update(UBViewModel tablero) {
        try{ 
        if(!ModelState.IsValid) return RedirectToAction("Index");
         var newTablero = new Tablero() {
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion,
            IdUsuarioPropietario = tablero.IdUsuarioPropietario
        };
        repo.Update(tablero.Id, newTablero);
        return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
     [HttpGet]
    public IActionResult Delete(int id) {
        try{
            if(!ModelState.IsValid || !esAdmin()) return RedirectToAction("Index");
             repo.Delete(id);
            return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
        
       
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