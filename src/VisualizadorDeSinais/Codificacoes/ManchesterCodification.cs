using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualizadorDeSinais.Codificacoes;

/// <summary>
/// Implementa a codificacao de Manchester.
/// </summary>
internal class ManchesterCodification : ILineCodification {

	public string UserFriendlyName => "Manchester";

    public string Description => "Manchester\n" +
        "Cada bit é representado por dois níveis de tensão, um no meio do bit e outro no início/fim do bit.";

    public List<int> Codify(List<int> bitSequence){
		// sinal positivo: no meio do bit sinal vai de -v para +v
		// sinal negativo: no meio do bit sinal vai de +v para -v

		List<int> manchesterSequence = [];

		for(int i = 0; i < bitSequence.Count; i++){
			if (bitSequence[i] == 0){
				manchesterSequence.Add(1);
				manchesterSequence.Add(-1);
			}
			else{
				manchesterSequence.Add(-1);
				manchesterSequence.Add(1);
			}
		}
		return manchesterSequence;
		}

    public double GetFrequency(){
        return 0.5;
    }

    public List<int> GetStates(){
        return [-2, -1, 0, 1, 2];
    }
}

