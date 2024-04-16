using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

/// <summary>
/// Implementa a codificacao de linha NRZ-L.
/// </summary>
/// <remarks>NRZ-L significa No Return to Zero - Level</remarks>
internal class NRZLCodification : ILineCodification {
    public List<int> Codify(List<int> bitSequence) {
        return bitSequence
            .Select(x => x == 1 ? -1 : 1)
            .ToList();
    }

    public int GetFrequency() {
        return 1;
    }

    public List<int> GetStates() {
        return [-2, -1, 0, 1, 2];
    }
}
