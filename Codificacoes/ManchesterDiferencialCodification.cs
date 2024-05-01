using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

internal class ManchesterDiferencialCodification : ILineCodification{
    public List<int> Codify(List<int> bitSequence)
    {
        List<int> manchesterSequence = [];

        for (int i = 0; i < bitSequence.Count; i++)
        {
            int next = bitSequence[i];
            int last = manchesterSequence.Count > 0 ? manchesterSequence[^1] : 1;
            if (next == 1)
            {
                manchesterSequence.Add(last);
                manchesterSequence.Add(last * -1);
            }
            else
            {
                manchesterSequence.Add(last * -1);
                manchesterSequence.Add(last);
            }
        }
        return manchesterSequence;
    }

    public double GetFrequency()
    {
        return 0.5;
    }

    public List<int> GetStates()
    {
        return [-1, 0, 1];
    }
}