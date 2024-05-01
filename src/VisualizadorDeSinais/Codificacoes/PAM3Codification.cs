using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;
internal class PAM3Codification : ILineCodification {
    public string UserFriendlyName => "PAM 3";

    public string CompleteName => "Pulse Amplitude Modulation 3";

    public string Description => "Cada grupo de 3 bits é mapeado para dois símbolos de 3 estados(-V,0V,+V) cada. " +
        "Ínicio(SSD) e fim(ESD) de transmissões são representados por 2 sinais 0V seguidos.";

    private readonly Dictionary<(int, int, int), (int, int)> table = new (){
        { (0,0,0), (-1,-1) },
        { (0,0,1), (-1,0) },
        { (0,1,0), (-1,1) },
        { (0,1,1), (0,-1) },
        { (1,0,0), (0,1) },
        { (1,0,1), (1,-1) },
        { (1,1,0), (1,0) },
        { (1,1,1), (1,1) }
    };

    private readonly (int,int) startStreamDelimiter = (0,0);
    private readonly (int,int) endStreamDelimiter = (0,0);

    public List<int> Codify(List<int> bitSequence) {
        List<int> newSeq = [];

        newSeq.Add(startStreamDelimiter.Item1);
        newSeq.Add(startStreamDelimiter.Item2);

        // add padding if necessary
        if(bitSequence.Count % 3 != 0) {
            bitSequence.AddRange(new int[3 - bitSequence.Count % 3]);
        }

        for (int i = 0; i < bitSequence.Count; i += 3) {
            int a = bitSequence[i];
            int b = bitSequence[i + 1];
            int c = bitSequence[i + 2];

            (int, int) result = table[(a, b, c)];

            newSeq.Add(result.Item1);
            newSeq.Add(result.Item2);
        }

        newSeq.Add(endStreamDelimiter.Item1);
        newSeq.Add(endStreamDelimiter.Item2);

        return newSeq;
    }

    public double GetFrequency() => 0.5;

    public List<int> GetStates() => [-2, -1, 0, 1, 2];
}
