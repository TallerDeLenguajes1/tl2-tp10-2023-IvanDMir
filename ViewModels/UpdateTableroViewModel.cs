using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class UBViewModel {
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public UBViewModel(Tablero tablero) {
        Id = tablero.IdTablero;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}