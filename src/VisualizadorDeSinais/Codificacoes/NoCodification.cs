using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes; 
internal class NoCodification : ILineCodification {
    public string UserFriendlyName => "";

    public string Description => "";

    public List<int> Codify(List<int> bitSequence) {
        return bitSequence;
    }

    public double GetFrequency() {
        return 1;
    }

    public List<int> GetStates() {
        return new List<int> { -1, 1 };
    }
}
