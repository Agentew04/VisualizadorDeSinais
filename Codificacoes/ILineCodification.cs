using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes; 
internal interface ILineCodification {

    /// <summary>
    /// Transforma uma sequencia de bits
    /// </summary>
    /// <param name="bitSequence"></param>
    /// <returns></returns>
    public List<char> Codify(List<char> bitSequence);

}
