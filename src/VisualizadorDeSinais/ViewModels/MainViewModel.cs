using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VisualizadorDeSinais.Codificacoes;

namespace VisualizadorDeSinais.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public List<ILineCodification> Codifications { get; } = [
        new AMICodification(),
        new CMICodification(),
        new ManchesterCodification(),
        new ManchesterDiferencialCodification(),
        new NRZICodification(),
        new NRZLCodification(),
        new PseudoTernaryCodification()
    ];

    [RelayCommand]
    public async Task Codify() {

    }
}
