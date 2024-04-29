using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes; 
public interface ILineCodification {

    /// <summary>
    /// Transforma uma sequencia de bits para sinais digitais
    /// </summary>
    /// <param name="bitSequence">Lista com os bits. O int vai ser <b>sempre</b>
    /// ou <see langword="1"/> ou <see langword="0"/>.</param>
    /// <returns>Uma lista com os niveis correspondentes do sinal gerado.</returns>
    public List<int> Codify(List<int> bitSequence);

    /// <summary>
    /// A frequencia final desse metodo. Se for igual ao
    /// inicial, deve ser 1. Se for o dobro, como no caso
    /// do Manchester diferencial, deve ser 2.
    /// </summary>
    public double GetFrequency();

    /// <summary>
    /// Deve retornar os estados possiveis da codificacao.
    /// Ex: -1, 0 , 1. Isso vai ser interpretado como Volts
    /// </summary>
    /// <returns>Lista com todos os estados possíveis ordenados.
    /// Pode ser adicionado um padding de sinais extras antes e
    /// depois para melhor legibilidade do gráfico final.</returns>
    public List<int> GetStates();

    public string UserFriendlyName { get; }

}
