using ContaCorrente.Domain.Entities.CommandsInterfaces;
using ContaCorrente.Domain.Enums;
using ContaCorrente.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContaCorrente.Domain.Entities
{
    public class ContaCorrente
    {
        public int numeroConta { get;  set; }
        public decimal saldoContaCorrente { get;  set; }
        public ICollection<Lancamentos> lancamentos { get; set; } = new List<Lancamentos>();


        /// <summary>
        /// precisei expor pois usei na minha classe de Data, no DDD nao se expoe o dominio
        /// </summary>
        public ContaCorrente()
        {
        }


        /// <summary>
        /// Faz as primeiras validacoes de campos obrigatorios
        /// </summary>
        /// <param name="command"></param>
        public ContaCorrente(IContaCorrenteCommand command)
        {
            var msgs = Validar(command);

                 if (msgs.Count > 0)
                throw new DomainValidationException(msgs);
        }


        /// <summary>
        /// Evento de credito em contacorrente
        /// </summary>
        /// <param name="command"></param>
        public void CreditoContaCorrente(IContaCorrenteCommand command)
        {
            var msgs = this.ValidarCredito(this);

            if (msgs.Count > 0)
                throw new DomainValidationException(msgs);

            var lancamento = new Lancamentos(TipoLancamento.Credito, command.valorLancamento);

            this.AdicionarLancamento(lancamento);

            this.saldoContaCorrente = this.lancamentos.Sum(x => x.valorLancamento);

        }


        /// <summary>
        /// Evento de debito em contacorrente
        /// </summary>
        /// <param name="command"></param>
        public void DebitoContaCorrente(IContaCorrenteCommand command)
        {

            var lancamento = new Lancamentos(TipoLancamento.Debito , command.valorLancamento * -1);

            this.AdicionarLancamento(lancamento);

            this.saldoContaCorrente = this.lancamentos.Sum(x => x.valorLancamento);

            var msgs = this.ValidarDebito(this);

            if (msgs.Count > 0)
                throw new DomainValidationException(msgs);

        }


        protected List<DomainValidationMessage> Validar(IContaCorrenteCommand command)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();

            if (command.numeroContaDestino == 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error,  nameof(command.numeroContaDestino), "Numero da conta Destino obrigatorio"));

            if (command.numeroContaOrigem == 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error,  nameof(command.numeroContaOrigem), "Numero da conta Origem obrigatorio"));

            if (command.valorLancamento <= 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error,  nameof(command.valorLancamento), "Valor da transferencia menor ou igual a 0"));


            if (command.numeroContaDestino == command.numeroContaOrigem)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, nameof(command.valorLancamento), "Conta Origem e Conta Destino sao iguais"));

            return messages;
        }

        protected List<DomainValidationMessage> ValidarCredito(ContaCorrente conta)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();

            if (conta == null)
            {
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, "numeroContaDestino", "Conta destino inexistente. Tente Novamente"));
            }

            return messages;
        }


        protected List<DomainValidationMessage> ValidarDebito(ContaCorrente conta)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();

            if(conta.saldoContaCorrente <= 0)
            {
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, "saldoContaCorrente", "Conta Origem sem saldo para Debito em CC"));
            }

            return messages;
        }


        protected void AdicionarLancamento(Lancamentos lancamentos)
        {
            this.lancamentos.Add(lancamentos);
        }


    }
}
