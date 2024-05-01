using Avalonia.Controls.Notifications;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using VisualizadorDeSinais.Codificacoes;

namespace VisualizadorDeSinais.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public List<ILineCodification> Codifications { get; } = [
        new NRZLCodification(),
        new NRZICodification(),
        new ManchesterCodification(),
        new ManchesterDiferencialCodification(),
        new AMICodification(),
        new PseudoTernaryCodification(),
        new CMICodification(),
        new HDB3Codification()
    ];

    [ObservableProperty]
    private ILineCodification? selectedCodification;

    [ObservableProperty]
    private string binaryText = "";

    [ObservableProperty]
    private ObservableCollection<ISeries> chartSeries = [];

    [ObservableProperty]
    private ObservableCollection<Axis> axisX = [];

    [ObservableProperty]
    private ObservableCollection<Axis> axisY = [];

    private Func<double, string> labelerXAxis { get; } = value => $"{value}s";

    private Func<double, string> labelerYAxis { get; } = value => $"{value}V";

    [RelayCommand]
    public void Codify() {
        // converter string para lista de bits
        List<int> bitSequence = BinaryText.AsEnumerable()
            .Select(x => x == '1' ? 1 : 0).ToList();

        if(SelectedCodification is null) {
            return;
        }
        List<int > codified = SelectedCodification.Codify(bitSequence);

        // cria a serie principal de pontos
        var points = new List<ObservablePoint>(
            codified.Select((x, i) => new ObservablePoint((i) * SelectedCodification.GetFrequency(), x))
        );
        var lastPoint = points.LastOrDefault();
        if (lastPoint is not null) {
            points.Add(new ObservablePoint(lastPoint.X + SelectedCodification.GetFrequency(), lastPoint.Y));
        }

        var series = new StepLineSeries<ObservablePoint> {
            Values = points
        };
        ChartSeries.Clear(); // remove a serie anterior
        ChartSeries.Add(series);

        // cria os eixos e seta max/min
        var x = new Axis {
            Labeler = labelerXAxis,
            MinStep = SelectedCodification.GetFrequency(),
            MinLimit = 0
        };
        var y = new Axis {
            Labeler = labelerYAxis,
            MinStep = 1,
            MaxLimit = SelectedCodification.GetStates().Max(),
            MinLimit = SelectedCodification.GetStates().Min()
        };
        AxisX.Clear();
        AxisX.Add(x);
        AxisY.Clear();
        AxisY.Add(y);
    }
}
