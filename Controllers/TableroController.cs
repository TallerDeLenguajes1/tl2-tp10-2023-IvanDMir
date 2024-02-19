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
                 if(!isLogin()){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
               
           
            }else if(esAdmin()){
                 var IdUsuarioLogueado = Convert.ToInt32(HttpContext.Session.GetString("Id"));
                GBViewModel viewTableros = new GBViewModel(repo.GetAll(),repo.GetByUser(IdUsuarioLogueado),repo.GetByTarea(IdUsuarioLogueado),IdUsuarioLogueado);
                return View(viewTableros);
            }else{
                return RedirectToRoute(new { controller = "Tablero", action = "IndexOperador"});
            }
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }

    [HttpGet]

    public IActionResult IndexOperador(){
        try{
            if(!isLogin()){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
            var IdUsuarioLogueado = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            GBViewModel viewTableros = new GBViewModel(repo.GetByUser(IdUsuarioLogueado),repo.GetByTarea(IdUsuarioLogueado),IdUsuarioLogueado);
            return View(viewTableros);
        }catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
    }
    }

    [HttpGet]
    public IActionResult Add(){
        try{ 
            if(!isLogin()){
                return RedirectToRoute(new{controller = "Login", action = "Index"});
            }
           return View(new ABViewModel(Convert.ToInt32(HttpContext.Session.GetString("Id"))));
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
              return View(new UBViewModel(repo.GetById(id)));
            
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }            


    } 


    [HttpPost]
    public IActionResult Update(UBViewModel tablero) {
        try{ 
        if(!ModelState.IsValid) return RedirectToAction("Index");
        var viejoTablero =repo.GetById(tablero.Id);
         var newTablero = new Tablero() {
            IdTablero = tablero.Id,
            Nombre = tablero.Nombre,
            Descripcion = tablero.Descripcion,
            IdUsuarioPropietario = viejoTablero.IdUsuarioPropietario
        };
        repo.Update(newTablero.IdTablero, newTablero);
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
        if (HttpContext.Session.GetString("Rol") != null ){
            return true;
        }else{
            return false;
        }
    }

       private bool esAdmin(){
        var rol =HttpContext.Session.GetString("Rol");
         if ( rol  == "admin" ){
            return true;
         }
         return false;
    }
}