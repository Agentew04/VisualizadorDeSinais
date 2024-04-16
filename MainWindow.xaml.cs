using LiveCharts;
using LiveCharts.Wpf;
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
    public Func<double, string> YFormatter { get; set; }

    /// <summary>
    /// Textos que aparecem no eixo X
    /// </summary>
    public string[] Labels { get; set; }

    #endregion

    public MainWindow() {
        // componentes do front estao disponiveis apos essa call
        InitializeComponent();

        // registra todos os servicos de codificacao
        codificationProvider
            .RegisterCodification<NoCodification>("");

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

        ILineCodification? codification = codificationProvider.GetCodification(/*selected.Content.ToString() ?? */"");
        if (codification is null) { 
            MessageBox.Show(
                "Modo de codificação não encontrado",
                "Erro!",
                MessageBoxButton.OK,
                MessageBoxImage.Exclamation
            );
            return;
        }

        List<int> codified = codification.Codify(bits);

        // montar a visualizacao
        var series = new StepLineSeries {
            Title = $"Codificação {selected.Content}",
            Values = new ChartValues<int>(codified)
        };
        SeriesCollection.Clear();
        SeriesCollection.Add(series);
        Labels = Enumerable.Range(0, codified.Count).Select(x => x.ToString()).ToArray();
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

}