using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class UTViewModel {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public Estados Estado { get; set; }
    public string Descripcion { get; set; }
    public string Color { get; set; }

    public UTViewModel(Tarea tarea) {
        Id = tarea.Id;
        Nombre = tarea.Nombre;
        Estado = tarea.Estado;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
    }
}