using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_IvanDMir.Models;


namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class AsTVM {
    public int Idtarea { get; set; }


    public List<Usuario> UsuariosDisponibles {get;set;}
    public int usuario { get; set; }

    public AsTVM() {}

    public AsTVM(int IdTarea,List<Usuario> usuariosDisponibles) {
        Idtarea = IdTarea;
        UsuariosDisponibles = usuariosDisponibles;
    }
}