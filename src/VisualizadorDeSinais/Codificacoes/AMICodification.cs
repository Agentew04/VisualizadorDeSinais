using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

/// <summary>
/// Implementa a codificacao de linha AMI.
/// </summary>
internal class AMICodification : ILineCodification {

    public string UserFriendlyName => "AMI";

    public List<int> Codify(List<int> bitSequence) {
        int lastPositiveState = 1; // 1 para positivo e -1 para negativo   

        List<int> newSeq = [];
        for(int i=0; i<bitSequence.Count; i++) {
            if (bitSequence[i] == 1) {
                newSeq.Add(lastPositiveState);
                lastPositiveState *= -1;
            } else {
                newSeq.Add(0);
            }
        }
        return newSeq;
    }

    public double GetFrequency() {
        return 1;
    }

    public List<int> GetStates() {
        return [-2, -1, 0, 1, 2];
    }
}
