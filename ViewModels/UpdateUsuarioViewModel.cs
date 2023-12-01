using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class UUViewModel {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Contrasena { get; set; }
    public Roles Rol {get;set;}


    public UUViewModel(){

    }
    public UUViewModel(Usuario user) {
        Id = user.id_usuario;
        Nombre = user.nombre_De_Usuario;
        Contrasena = user.contrasena;
        Rol = user.rol;
    }
}