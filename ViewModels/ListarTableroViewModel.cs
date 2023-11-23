using tl2_tp10_2023_IvanDMir.Models;

namespace tl2_tp10_2023_IvanDMir.ViewModels;

public class GBViewModel {
    public List<Tablero> Tableros { get; set; }

    public GBViewModel(List<Tablero> tableros) {
        Tableros = tableros;
    }
}