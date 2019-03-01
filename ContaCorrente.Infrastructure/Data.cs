using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Infrastructure
{
    public static class Data
    {
        public static List<ContaCorrente.Domain.Entities.ContaCorrente> DataContacorrente()
        {
            var listaCC = new List<ContaCorrente.Domain.Entities.ContaCorrente>();

            listaCC.Add(new Domain.Entities.ContaCorrente { numeroConta = 123456789, saldoContaCorrente = 5000, lancamentos = new List<Lancamentos>() { new Lancamentos(TipoLancamento.Credito, 10000), new Lancamentos(TipoLancamento.Debito , -5000) } });

            listaCC.Add(new Domain.Entities.ContaCorrente { numeroConta = 789654321, saldoContaCorrente = 20000, lancamentos = new List<Lancamentos>() { new Lancamentos(TipoLancamento.Credito, 10000), new Lancamentos(TipoLancamento.Credito, 10000) } });

            listaCC.Add(new Domain.Entities.ContaCorrente { numeroConta = 654321789, saldoContaCorrente = 200000, lancamentos = new List<Lancamentos>() { new Lancamentos(TipoLancamento.Credito, 100000), new Lancamentos(TipoLancamento.Credito, 100000) } });


            return listaCC;
        }

    }
}
