using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;

namespace tl2_tp10_2023_IvanDMir.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    TareaRepositorios repo;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        repo = new TareaRepositorios();
    }

        public IActionResult Index()
    {
        List<Tarea> Tareas = repo.GetAll();
        return View(Tareas);
    
    }

     [HttpGet]
    public IActionResult Add(){
        return View(new Tarea());
    }
    [HttpPost]
    public IActionResult Add(Tarea tab){
        repo.Crear(tab);
         return RedirectToAction("Index");
    }
     [HttpGet]
    public IActionResult Update(int id){
        return View(repo.GetById(id));
    }
    [HttpPost]
    public IActionResult Update(Tarea tarea){
        repo.Update(tarea.Id,tarea);
        return RedirectToAction("Index");
        

    }
    [HttpGet]
    public IActionResult Delete(int id){  
        repo.Delete(id);
      return RedirectToAction("Index");
    }
}