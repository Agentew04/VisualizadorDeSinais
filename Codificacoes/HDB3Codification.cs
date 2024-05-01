using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

/// <summary>
/// Implementa a codificação HDB3 (High-Density Bipolar 3 Zeros).
/// </summary>
internal class HDB3Codification : ILineCodification
{
    public List<int> Codify(List<int> bitSequence)
    {
        List<int> newSeq = new List<int>();

        string hdb3stream = "";

        // Convertendo a lista de inteiros em uma string de bits
        foreach (int bit in bitSequence)
        {
            hdb3stream += bit.ToString();
        }

        int index0;
        index0 = hdb3stream.IndexOf("0000");
        int index1 = 0;

        int signal = 0;
        char last1bit = '0';
        char lastbit = '0';

        while (index0 != -1)
        {
            if ((index0 - index1) % 2 == 1)
                hdb3stream = hdb3stream.Substring(0, index0) + "000V" + hdb3stream.Substring(index0 + 4);
            else
                hdb3stream = hdb3stream.Substring(0, index0) + "B00V" + hdb3stream.Substring(index0 + 4);

            index1 = index0 + 4;

            index0 = hdb3stream.IndexOf("0000");
        }

        for (int pos = 0; pos < hdb3stream.Length; pos++)
        {
            if (hdb3stream[pos] == '1')
            {
                if (signal % 2 == 0)
                {
                    last1bit = '+';
                }
                else
                {
                    last1bit = '-';
                }

                lastbit = last1bit;
                signal++;
                hdb3stream = hdb3stream.Substring(0, pos) + lastbit + hdb3stream.Substring(pos + 1);
            }
            else if (hdb3stream[pos] == 'V')
            {
                hdb3stream = hdb3stream.Substring(0, pos) + lastbit + hdb3stream.Substring(pos + 1);
            }
            else if (hdb3stream[pos] == 'B')
            {
                if (last1bit == '+')
                    lastbit = '-';
                else
                    lastbit = '+';
                signal++;
                hdb3stream = hdb3stream.Substring(0, pos) + lastbit + hdb3stream.Substring(pos + 1);
            }
        }
        for (int i = 0; i < hdb3stream.Length; i++)
        {
            if (hdb3stream[i] == '+')
            {
                newSeq.Add(1);
            }
            else if (hdb3stream[i] == '-')
            {
                newSeq.Add(-1);
            }
            else
            {
                newSeq.Add(0);
            }
        }
        return newSeq;
    }

    public double GetFrequency()
    {
        // Frequência não é relevante para a codificação HDB3, então retorne o valor que fizer sentido para sua aplicação.
        return 1;
    }

    public List<int> GetStates()
    {
        return new List<int> { -2, -1, 0, 1, 2 };
    }
}