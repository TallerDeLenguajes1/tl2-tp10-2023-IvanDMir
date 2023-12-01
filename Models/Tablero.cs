namespace tl2_tp10_2023_IvanDMir.Models;
 public class Tablero{
    private int id_tablero;
    private string nombre;
    private string descripcion;
    private int id_usuario_propietario;

     public int IdTablero
    {
        get { return id_tablero; }
        set { id_tablero = value; }
    }

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }

    public int IdUsuarioPropietario
    {
        get { return id_usuario_propietario; }
        set { id_usuario_propietario = value; }
    }
}