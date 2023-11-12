using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using  tl2_tp10_2023_IvanDMir.Models;
using tl2_tp10_2023_IvanDMir.repositorios;

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

        public IActionResult Index()
    {
        List<Tablero> tableros = repo.GetAll();
        return View(tableros);
    
    }

     [HttpGet]
    public IActionResult Add(){
        return View(new Tablero());
    }
    [HttpPost]
    public IActionResult Add(Tablero tab){
        repo.Crear(tab);
         return RedirectToAction("Index");
    }
     [HttpGet]
    public IActionResult Update(int id){
        return View(repo.GetById(id));
    }
    [HttpPost]
    public IActionResult Update(Tablero tab){
        repo.Update(tab.IdTablero,tab);
        return RedirectToAction("Index");
        

    }
}