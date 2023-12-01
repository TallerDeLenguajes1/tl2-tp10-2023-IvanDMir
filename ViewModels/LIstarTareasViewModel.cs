using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class GTViewModel {
    public List<Tarea> Tareas { get; set; }

    public GTViewModel(){
        
    }
    public GTViewModel(List<Tarea> tareas) {
        Tareas = tareas;
    }
}