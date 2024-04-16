using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes; 
internal interface ILineCodification {

    /// <summary>
    /// Transforma uma sequencia de bits para sinais digitais
    /// </summary>
    public List<int> Codify(List<int> bitSequence);

    /// <summary>
    /// A frequencia final desse metodo. Se for igual ao
    /// inicial, deve ser 1. Se for o dobro, como no caso
    /// do Manchester diferencial, deve ser 2.
    /// </summary>
    public int GetFrequency();

    /// <summary>
    /// Deve retornar os estados possiveis da codificacao.
    /// Ex: -1, 0 , 1. Isso vai ser interpretado como Volts
    /// </summary>
    public List<int> GetStates();

}
