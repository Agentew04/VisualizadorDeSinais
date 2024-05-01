using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using VisualizadorDeSinais.Codificacoes;

namespace VisualizadorDeSinais;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

    #region Variables

    /// <summary>
    /// Gerenciador de codificacoes
    /// </summary>
    private CodificationProvider codificationProvider = new();

    /// <summary>
    /// Lista de Series no grafico
    /// </summary>
    public SeriesCollection SeriesCollection { get; set; }

    /// <summary>
    /// Lambda para formatar o texto dos valores no eixo Y
    /// </summary>
    public Func<double, string> YFormatter { get; set; } = x => $"{(int)Math.Round(x)} V";

    /// <summary>
    /// Textos que aparecem no eixo X
    /// </summary>
    public string[] Labels { get; set; }

    #endregion

    public MainWindow() {
        // componentes do=ont estao disponiveis apos essa call
        InitializeComponent();

        // registra todos os servicos de codificacao
        codificationProvider
            .RegisterCodification<NoCodification>("") // <- remover essa linha antes de enviar
            .RegisterCodification<NRZLCodification>("NRZ-L")
            .RegisterCodification<NRZICodification>("NRZ-I")
            .RegisterCodification<AMICodification>("AMI")
            .RegisterCodification<PseudoTernaryCodification>("Pseudoternário")
            .RegisterCodification<ManchesterCodification>("Manchester")
            .RegisterCodification<ManchesterDiferencialCodification>("Manchester Diferencial")
            .RegisterCodification<CMICodification>("CMI")
            .RegisterCodification<HDB3Codification>("HDB3")
            .RegisterCodification<DOISB1QCodification>("2BQ1");
            

        SeriesCollection = [];

        // Define o contexto das Bindings para o xaml e code-behind
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

        ILineCodification? codification = codificationProvider.GetCodification(selected.Content.ToString() ?? "");
        if (codification is null) { 
            MessageBox.Show(
                "Modo de codificação não encontrado",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
            );
            return;
        }

        if (bits.Count == 0)
        {
            MessageBox.Show(
                "Insira uma sequência de bits",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
        );
            return;
        }

        List<int> codified = codification.Codify(bits);

        var points = new ChartValues<ObservablePoint>(codified.Select((x, i) => new ObservablePoint((i) * codification.GetFrequency(), x)));
            
        // montar a visualizacao
        var series = new StepLineSeries {
            Title = $"Codificação {selected.Content}",
            Values = points,
        };
        chart.AxisY[0].MinValue = codification.GetStates().Min();
        chart.AxisY[0].MaxValue = codification.GetStates().Max();
        chart.AxisY[0].Separator.Step = 1;
        SeriesCollection.Clear();
        //SeriesCollection.Add(new LineSeries {
        //    Title = "Zero",
        //    Values = new ChartValues<int>(codified.Select(x => 0)),
        //    Fill = System.Windows.Media.Brushes.Transparent,
        //    StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 2 }),
        //    StrokeThickness = 1,
        //    PointGeometry = null,
        //    Stroke = System.Windows.Media.Brushes.Black
        //});
        SeriesCollection.Add(series);
        //chart.AxisX[0].Separator.Step = codification.GetFrequency();
        //Labels = Range(0, codified.Count, codification.GetFrequency()).Select(x => x.ToString(CultureInfo.InvariantCulture)).ToArray();
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

    private static IEnumerable<double> Range(double start, double count, double step)
    {
        for (int i = 0; i < count; i++)
        {
            yield return start + i * step;
        }
        
    }

    private void OnSelectChange(object sender, SelectionChangedEventArgs e)
    {
        List<int> bits = GetBitSequence();

        if (codificacaoComboBox.SelectedItem is not ComboBoxItem selected)
        {
            MessageBox.Show(
                "Selecione um modo de codificação",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
            );
            return;
        }

        ILineCodification? codification = codificationProvider.GetCodification(selected.Content.ToString() ?? "");
        if (codification is null)
        {
            MessageBox.Show(
                "Modo de codificação não encontrado",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
            );
            return;
        }

        if (bits.Count == 0)
        {
            MessageBox.Show(
                "Insira uma sequência de bits",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
        );
            return;
        }

        List<int> codified = codification.Codify(bits);

        var points = new ChartValues<ObservablePoint>(codified.Select((x, i) => new ObservablePoint((i) * codification.GetFrequency(), x)));

        // montar a visualizacao
        var series = new StepLineSeries
        {
            Title = $"Codificação {selected.Content}",
            Values = points,
        };
        chart.AxisY[0].MinValue = codification.GetStates().Min();
        chart.AxisY[0].MaxValue = codification.GetStates().Max();
        chart.AxisY[0].Separator.Step = 1;
        SeriesCollection.Clear();
        SeriesCollection.Add(series);
    }

}