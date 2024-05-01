using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

/// <summary>
/// Implementa a codificacao de linha Pseudo-Ternary.
/// É basicamente o inverso do AMI
/// </summary>
internal class PseudoTernaryCodification : ILineCodification {

    public string UserFriendlyName => "Pseudo-Ternary";

    public string CompleteName => "Pseudo-Ternary";

    public string Description => "Bits 0 são voltagem 0V e bits 1 alternam ente +V e -V. " +
        "Basicamente o inverso do AMI";

    public List<int> Codify(List<int> bitSequence) {
        int lastPositiveState = 1; // 1 para positivo e -1 para negativo   

        List<int> newSeq = [];
        for (int i = 0; i < bitSequence.Count; i++) {
            if (bitSequence[i] == 0) {
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

    public List<int> GetStates() => [-2, -1, 0, 1, 2];
}
