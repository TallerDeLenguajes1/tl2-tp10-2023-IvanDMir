namespace tl2_tp10_2023_IvanDMir.Models;
public enum Estados{
    Pendiente,
    Haciendo,
    revisión,
    Finalizada
}
public class Tarea{
    private int id;
    private int idTablero;
    private string nombre;
    private Estados estado;
    private string descripcion;
    private string color;
    private int idUsuarioAsignado;
    private int IdPropietario;

     public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public int IdTablero
    {
        get { return idTablero; }
        set { idTablero = value; }
    }

    public string Nombre
    {
        get { return nombre; }
        set { nombre = value; }
    }

    public Estados Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public string Descripcion
    {
        get { return descripcion; }
        set { descripcion = value; }
    }

    public string Color
    {
        get { return color; }
        set { color = value; }
    }

    public int IdUsuarioAsignado
    {
        get { return idUsuarioAsignado; }
        set { idUsuarioAsignado = value; }
    }
    public int idPropietario{
        get { return IdPropietario;}
        set { IdPropietario = value;}
    }
}