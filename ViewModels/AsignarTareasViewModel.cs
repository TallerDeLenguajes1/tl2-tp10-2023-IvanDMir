using System.ComponentModel.DataAnnotations;


namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class AsTVM {
    public int Idtarea { get; set; }

    [Required(ErrorMessage = "Campo requerido")]
    public string usuario { get; set; }

    public AsTVM() {}

    public AsTVM(int IdTarea) {
        Idtarea = IdTarea;
    }
}