using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class LUViewModel {
    public List<Usuario> Usuarios { get; set; }

    public LUViewModel(List<Usuario> usuarios) {
        Usuarios = usuarios;
    }
}