using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public List<string> Codifications { get; } = new List<string> {
        "a",
        "b", "c"
    };

    [RelayCommand]
    public async Task Codify() {

    }
}
