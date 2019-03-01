using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Domain.Entities.CommandsInterfaces
{
    public interface IContaCorrenteCommand
    {
        int numeroContaOrigem { get; set; }
        int numeroContaDestino { get; set; }
        decimal valorLancamento { get; set; }
    }
}
