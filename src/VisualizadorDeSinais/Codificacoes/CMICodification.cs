using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;
internal class CMICodification : ILineCodification {

    public string UserFriendlyName => "CMI";

    public List<int> Codify(List<int> bitSequence) {
        int lastPositiveState = -1;
        List<int> newSeq = [];

        foreach(int bit in bitSequence) {
            if (bit == 0) {
                newSeq.Add(-1);
                newSeq.Add(1);
            } else {
                newSeq.Add(lastPositiveState);
                newSeq.Add(lastPositiveState);
                lastPositiveState *= -1;
            }
        }
        return newSeq;
    }

    public double GetFrequency() => 0.5f;

    public List<int> GetStates() => [-2, -1, 0, 1, 2];
}
