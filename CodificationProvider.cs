using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualizadorDeSinais.Codificacoes;

namespace VisualizadorDeSinais; 
internal class CodificationProvider {

    private Dictionary<string, ILineCodification> codifications = [];

    public CodificationProvider RegisterCodification<T>(string identifier) where T : ILineCodification, new() {
        ILineCodification codification = new T();
        codifications.Add(identifier, codification);
        return this;
    }

    public ILineCodification? GetCodification(string codificationName) {
        if (!codifications.TryGetValue(codificationName, out ILineCodification? value)) {
            return null;
        }
        return value;
    }
}
