using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContaCorrente.Infrastructure.DataAcess.Repository
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        /// <summary>
        /// Faz a busca da conta corrente e seus lancamentos pelo numeroConta
        /// </summary>
        /// <param name="numeroConta"></param>
        /// <returns></returns>
        public Domain.Entities.ContaCorrente BuscaContaCorrentePorNumeroConta(int numeroConta)
        {
            return Data.DataContacorrente().Where(x => x.numeroConta == numeroConta).FirstOrDefault();                
        }

    }
}
