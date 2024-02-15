using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class GBViewModel {
    public List<Tablero> TablerosAsignados { get; set; }
    public List<Tablero> TablerosConTareas {get;set;}
    public List<Tablero> TodosLosTableros{get;set;}

    public int idDueño{get;set;}
    public GBViewModel(){
        
    }

    public GBViewModel(List<Tablero> tableros,List<Tablero> tablerosAsignado,List<Tablero> tableroTareas,int IdDueño) {
        TodosLosTableros = tableros;
        TablerosConTareas = tableroTareas;
        TablerosAsignados = tablerosAsignado;
        idDueño = IdDueño;
    }
    public GBViewModel(List<Tablero> tablerosA,List<Tablero> tableroTareas,int IdDueño) {
        TodosLosTableros = null;
        TablerosConTareas = tableroTareas;
        TablerosAsignados = tablerosA;
        idDueño = IdDueño;
    }
}