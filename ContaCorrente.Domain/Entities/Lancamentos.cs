using ContaCorrente.Domain.Enums;
using ContaCorrente.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Domain.Entities
{
    public class Lancamentos
    {
        public TipoLancamento tipoLancamento { get; set; }
        public decimal valorLancamento { get; set; }

        public Lancamentos()
        {

        }

        public Lancamentos(TipoLancamento tipoLancamento, decimal valorLancamento)
        {
            var msgs = this.Validar(tipoLancamento, valorLancamento);

            if (msgs.Count > 0)
                throw new DomainValidationException(msgs);

            this.tipoLancamento = tipoLancamento;
            this.valorLancamento = valorLancamento;
        }

        /// <summary>
        /// Validar regras de lancamentos
        /// </summary>
        /// <param name="tipoLancamento"></param>
        /// <param name="valorLancamento"></param>
        /// <returns></returns>
        protected List<DomainValidationMessage> Validar(TipoLancamento tipoLancamento, decimal valorLancamento)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();

            if (tipoLancamento == TipoLancamento.Credito && valorLancamento <= 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, nameof(valorLancamento), "Valor do lancamento de credito, precisa ser maior que zero"));

            return messages;
        }

    }
}
