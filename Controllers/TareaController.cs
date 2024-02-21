using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;
using tl2_tp10_2023_IvanDMir.ViewModels;

namespace tl2_tp10_2023_IvanDMir.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareasRepositorio repo;
    private ITableroRepositorio tablerorepo;
    private IUsuarioRepositorio usuarioRepo;
    

    public TareaController(ILogger<TareaController> logger, ITareasRepositorio tareasRepositorio,ITableroRepositorio tableroRepositorio,IUsuarioRepositorio usuarioRepositorio)
    {
        _logger = logger;
        repo = tareasRepositorio;
        tablerorepo = tableroRepositorio;
        usuarioRepo = usuarioRepositorio;

    }

  [HttpGet]
    public IActionResult Index() {
        try { 
        
        if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
         var IdUsuarioLogueado = Convert.ToInt32(HttpContext.Session.GetString("Id"));
        
        if(esAdmin()) return View(new GTViewModel(repo.GetByUser(IdUsuarioLogueado),repo.GetByAsignado(IdUsuarioLogueado),repo.GetAll()));
                return RedirectToRoute(new { controller = "Tarea", action = "IndexOperador"});
        
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }
 [HttpGet]
    public IActionResult IndexOperador(){
        try{
            var IdUsuarioLogueado = Convert.ToInt32(HttpContext.Session.GetString("Id"));
             return View(new GTViewModel(repo.GetByUser(IdUsuarioLogueado),repo.GetByAsignado(IdUsuarioLogueado)));
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest();
        }

    }


 [HttpGet]
    public IActionResult Add(){

        try{ 
             if(!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
         return View(new ATViewModel(tablerorepo.GetByUser(Convert.ToInt32(HttpContext.Session.GetString("Id")))));
            
       }catch(Exception ex){
        _logger.LogError(ex.ToString());
         return BadRequest();
       }
       
    }

    [HttpPost]
    public IActionResult Add(ATViewModel tarea) {
        try { 
        if(!ModelState.IsValid) return View(tarea);
        var nuevaTarea = new Tarea() {
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion,
            Estado = Estados.Pendiente,
            Color = tarea.Color,
            IdTablero = tarea.IdTablero,
            idPropietario = Convert.ToInt32(HttpContext.Session.GetString("Id")),
            IdUsuarioAsignado = 0,
            NombreTablero = tablerorepo.GetById(tarea.IdTablero).Nombre
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
            var IdUsuarioLogueado = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            var tarea = repo.GetById(id);
            var TablerosDelUsuario = tablerorepo.GetByUser(IdUsuarioLogueado);
            foreach(Tablero tablero in TablerosDelUsuario){
                if(tablero.IdTablero == tarea.IdTablero){
                    var VM =new UTViewModel(tarea,true,HttpContext.Session.GetString("Rol"));
                    return View(VM);
                }
            }
            return View(new UTViewModel(tarea,false,HttpContext.Session.GetString("Rol")));
        }catch(Exception ex){
             _logger.LogError(ex.ToString());
             return BadRequest();

        }
    }
    [HttpPost]
    public IActionResult Update(UTViewModel tarea) {
        try { 
            var tareaVieja = repo.GetById(tarea.Id);
        if(!ModelState.IsValid)return View(tarea);
        var nuevaTarea = new Tarea() {
            Nombre = tarea.Nombre,
            Descripcion = tarea.Descripcion,
            Estado = tarea.Estado,
            Color = tarea.Color,
            IdTablero = tarea.IdTablero,
            idPropietario = Convert.ToInt32(HttpContext.Session.GetString("Id")),
            IdUsuarioAsignado = tareaVieja.IdUsuarioAsignado,
            NombreTablero = tablerorepo.GetById(tarea.IdTablero).Nombre

            
        };
        repo.Update(tarea.Id, nuevaTarea);
        return RedirectToAction("Index");
        }catch(Exception ex){
            _logger.LogError(ex.ToString());
             return BadRequest();
        }
    }
    [HttpGet]
    public IActionResult Asignar(int TareaId) {
        if (!isLogin()) return RedirectToRoute(new { controller = "Login", action = "Index"});
        return View(new AsTVM(TareaId,usuarioRepo.GetAll())); 
    }       

    [HttpPost]
    public IActionResult Asignar(AsTVM tarea) {
        if(!ModelState.IsValid) return RedirectToAction("Index");
        try {
            var usuarioElegido = usuarioRepo.GetById(tarea.usuario);
            repo.Asignar(usuarioElegido.id_usuario, tarea.Idtarea);
        } catch (Exception e) {
            _logger.LogError(e.ToString());
        }
        return RedirectToAction("Index");
        
    }



    [HttpGet]
    public IActionResult Delete(int id) {
        if(!esAdmin()) return RedirectToAction("Index");
        repo.Delete(id);
        return RedirectToAction("Index");
    }
         private bool isLogin()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Rol"))){
            return false;
        }else{
            return true;
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