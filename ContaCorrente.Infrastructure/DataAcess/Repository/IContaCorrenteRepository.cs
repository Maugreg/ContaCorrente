using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Infrastructure.DataAcess.Repository
{
    public interface IContaCorrenteRepository
    {
        Domain.Entities.ContaCorrente BuscaContaCorrentePorNumeroConta(int numeroConta);
    }
}
