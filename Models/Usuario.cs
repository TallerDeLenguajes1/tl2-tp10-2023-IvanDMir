namespace tl2_tp10_2023_IvanDMir.Models;
  public enum Roles
    {
        admin,
        operador
    }

public class Usuario{

    private int Id_usuario;
    private string Nombre_De_Usuario;
    private string Contrasena;
    private Roles Rol;
  

     public int id_usuario
    {
        get { return Id_usuario; }
        set { Id_usuario = value; }
    }
     public string nombre_De_Usuario
    {
        get { return Nombre_De_Usuario; }
        set { Nombre_De_Usuario = value; }
    }

    public string contrasena {
        get { return Contrasena; }
        set { Contrasena = value; }

    }
    public Roles rol {
         get { return Rol; }
        set { Rol = value; }
    }
    
  

}