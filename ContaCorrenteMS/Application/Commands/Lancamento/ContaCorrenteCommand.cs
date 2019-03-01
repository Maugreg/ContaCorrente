using ContaCorrente.Domain;
using ContaCorrente.Domain.Entities.CommandsInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaCorrenteMS.Application.Commands.Lancamento
{
    public class ContaCorrenteCommand : IRequest<Response>, IContaCorrenteCommand
    {
        public int numeroContaOrigem { get; set; }
        public int numeroContaDestino { get; set; }
        public decimal valorLancamento { get; set; }
    }
}
