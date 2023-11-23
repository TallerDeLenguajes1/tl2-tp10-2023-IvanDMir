using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class AUViewModel {
    public string Nombre { get; set; }
    public string Contrasena { get; set; }
    public Roles Rol { get; set; }

   
    public AUViewModel() {}
}