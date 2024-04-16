using LiveCharts;
using LiveCharts.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace VisualizadorDeSinais;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        // componentes do front estao disponiveis apos essa call
        InitializeComponent(); 

        SeriesCollection = new SeriesCollection
{
            new LineSeries
            {
                Title = "Series 1",
                Values = new ChartValues<double> { 4, 6, 5, 2 ,7 }
            },
            new LineSeries
            {
                Title = "Series 2",
                Values = new ChartValues<double> { 6, 7, 3, 4 ,6 }
            }
        };

        Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
        YFormatter = value => value.ToString("C");

        //modifying the series collection will animate and update the chart
        SeriesCollection.Add(new LineSeries {
            Values = new ChartValues<double> { 5, 3, 2, 4 },
            LineSmoothness = 0 //straight lines, 1 really smooth lines
        });

        //modifying any series values will also animate and update the chart
        SeriesCollection[2].Values.Add(5d);

        DataContext = this;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        List<int> bits = GetBitSequence();

        if (codificacaoComboBox.SelectedItem is not ComboBoxItem selected) {
            MessageBox.Show(
                "Selecione um modo de codificação",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
            );
            return;
        }

    }

    /// <summary>
    /// Retorna uma lista de 0 e 1 a partir do texto da caixa de texto
    /// </summary>
    private List<int> GetBitSequence() {
        return bitSequenceTextBox.Text
            .Where(x => x == '0' || x == '1')
            .Select(x => int.Parse(x.ToString()))
            .ToList();
    }

    public SeriesCollection SeriesCollection { get; set; }
    public string[] Labels { get; set; }
    public Func<double, string> YFormatter { get; set; }

}