using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class GTViewModel {
    public List<Tarea> TareasPropias { get; set; }
    public List <Tarea> TareasAsignadas {get;set;}
    public List<Tarea> TodasLasTareas{get;set;}

    public GTViewModel(){
        
    }
    public GTViewModel(List<Tarea> tareasPropias,List<Tarea> tareasAsignadas,List<Tarea> todas) {
        TareasPropias = tareasPropias;
        TareasAsignadas = tareasAsignadas;
        TodasLasTareas = todas;
    }
      public GTViewModel(List<Tarea> tareasPropias,List<Tarea> tareasAsignadas) {
        TareasPropias = tareasPropias;
        TareasAsignadas = tareasAsignadas;
        TodasLasTareas = null;
    }
}