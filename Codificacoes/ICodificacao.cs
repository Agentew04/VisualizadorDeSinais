using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes {
    internal interface ICodificacao {
        public string Codificar(List<char> bitSequence);
        public string Decodificar(List<char> bitSequence);
    }
}
