using ContaCorrente.Domain.Validation;
using ContaCorrente.Infrastructure.DataAcess.Repository;
using ContaCorrenteMS.Application.Commands.Lancamento;
using ContaCorrenteMS.Application.Mediator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContaCorrenteMS.Application.Commands.Lancamento
{
    public class ContaCorrenteHandler : AbstractRequestHandler<ContaCorrenteCommand>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ContaCorrenteHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        internal override HandleResponse HandleIt(ContaCorrenteCommand request, CancellationToken cancellationToken)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();
            
            // regras inicias de validacoes, de campos obrigatorios 
            var created = new ContaCorrente.Domain.Entities.ContaCorrente(request);

           
            var contaDebito = _contaCorrenteRepository.BuscaContaCorrentePorNumeroConta(request.numeroContaOrigem);
            //conta nao existe, excecao de entidade
            if(contaDebito == null)
            {
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, "numeroContaOrigem", "Conta origem inexistente. Tente Novamente"));
                throw new DomainValidationException(messages);
            }


            var contaCredito = _contaCorrenteRepository.BuscaContaCorrentePorNumeroConta(request.numeroContaOrigem);
            //conta nao existe, excecao de entidade
            if (contaCredito == null)
            {
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, "numeroContaDestino", "Conta destino inexistente. Tente Novamente"));
                throw new DomainValidationException(messages);
            }


            //regras de negocios do debito 
            contaDebito.DebitoContaCorrente(request);

            //regras de negocios do credito
            contaCredito.CreditoContaCorrente(request);


            //Caso nao houver excecao de dominio ou erro
            //persistiria na base.

            return new HandleResponse() { Content = contaCredito };

        }

    }
}
