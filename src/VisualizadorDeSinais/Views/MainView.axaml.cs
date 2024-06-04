using Avalonia.Controls;
using System.Linq;
using VisualizadorDeSinais.ViewModels;

namespace VisualizadorDeSinais.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void TextBox_TextChanged(object? sender, Avalonia.Controls.TextChangedEventArgs e) {
        if (sender is not TextBox box) {
            return;
        }
        MainViewModel? vm = DataContext as MainViewModel;

        if (vm == null) {
            return;
        }
        vm.BinaryText = new string((vm.BinaryText ?? "")
            .Where(x => x is '0' or '1').ToArray());
        vm.Codify();
    }

    private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e) {
        MainViewModel? vm = DataContext as MainViewModel;

        if (vm == null) {
            return;
        }
        vm.SelectedCodification = vm.Codifications.ElementAt((sender as ComboBox)?.SelectedIndex ?? 0);
        vm.Codify();
    }
}
