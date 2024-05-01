using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

internal class DOISB1QCodification : ILineCodification
{
    int previousLevel = 0;
    public List<int> Codify(List<int> bitSequence)
    {
        List<int> codifiedSequence = new List<int>();

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

    public double GetFrequency()
    {
        // Return the frequency for this codification (if applicable)
        return 1;
    }

    public List<int> GetStates()
    {
        // Return the possible states for this codification
        return new List<int> { -3, -1, 1, 3 };
    }
}