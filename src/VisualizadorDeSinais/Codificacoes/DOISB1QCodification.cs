using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

internal class DOISB1QCodification : ILineCodification {

    public string UserFriendlyName => "2B1Q";

    public string CompleteName => "2 Binary 1 Quaternary";

    public string Description => "Cada par de bits é mapeado para um nível de sinal quaternário.";

    int previousLevel = 1;
    public List<int> Codify(List<int> bitSequence)
    {
        List<int> codifiedSequence = [];

        // Loop through the input bit sequence
        for (int i = 0; i < bitSequence.Count; i += 2)
        {
            int firstBit = bitSequence[i];
            int secondBit = i + 1 < bitSequence.Count ? bitSequence[i + 1] : 0; // Assuming 0 if there's no second bit

            // Map the pair of bits to 2B1Q codeword
            int codeword = MapTo2B1Q(firstBit, secondBit);
            codifiedSequence.Add(codeword);
        }

        return codifiedSequence;
    }

    private int MapTo2B1Q(int bit1, int bit2)
    {
        if (bit1 == 0 && bit2 == 0)
        {
            if (previousLevel > 0)
            {
                previousLevel = 1;
                return 1;
            }
            else
            {
                previousLevel = -1;
                return -1;
            }
        }
        else if (bit1 == 0 && bit2 == 1)
        {
            if (previousLevel > 0)
            {
                previousLevel = 3;
                return 3;
            }
            else
            {
                previousLevel = -3;
                return -3;
            }
        }
        else if (bit1 == 1 && bit2 == 0)
        {
            if (previousLevel > 0)
            {
                previousLevel = -1;
                return -1;
            }
            else
            {
                previousLevel = 1;
                return 1;
            }
        }
        else if (bit1 == 1 && bit2 == 1)
        {
            if (previousLevel > 0)
            {
                previousLevel = -3;
                return -3;
            }
            else
            {
                previousLevel = 3;
                return 3;
            }
        }
        else
            throw new ArgumentException("Invalid bit pair for 2B1Q codification.");
    }

    public double GetFrequency() => 1;

    public List<int> GetStates() => [-4, -3, -1, 1, 3, 4];
}