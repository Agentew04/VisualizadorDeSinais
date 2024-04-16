using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;
internal class NRZICodification : ILineCodification {
    public List<int> Codify(List<int> bitSequence) {
        List<int> newSeq = [];
        for(int i=0; i<bitSequence.Count; i++) {
            int lastState = newSeq.Count > 0 ? newSeq[^1] : 1;
            if (bitSequence[i] == 1) {
                newSeq.Add(lastState * -1);
            } else { 
                newSeq.Add(lastState);
            }
        }
        return newSeq;
    }

    public int GetFrequency() {
        return 1;
    }

    public List<int> GetStates() {
        return [-2, -1, 0, 1, 2];
    }
}
