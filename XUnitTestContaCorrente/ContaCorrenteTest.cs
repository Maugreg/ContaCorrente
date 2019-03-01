using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Entities.CommandsInterfaces;
using ContaCorrente.Domain.Enums;
using ContaCorrente.Infrastructure.DataAcess.Repository;
using ContaCorrenteMS.Application.Commands.Lancamento;
using ContaCorrenteMS.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace XUnitTestContaCorrente
{
    public class ContaCorrenteTest
    {
   

        [Fact]
        public void TESTAR_CREDITO_CC()
        {

            var fakeMediator = new Mock<IContaCorrenteCommand>();

            fakeMediator.Setup(m => m.numeroContaOrigem).Returns(12346565);
            fakeMediator.Setup(m => m.numeroContaDestino).Returns(123456749);
            fakeMediator.Setup(m => m.valorLancamento).Returns(5000);


            var contaCorrente = new ContaCorrente.Domain.Entities.ContaCorrente
            {
                numeroConta = 123456749,
                saldoContaCorrente = 5000,
                lancamentos = new List<Lancamentos> { new Lancamentos(TipoLancamento.Credito, 5000)}

            };

            contaCorrente.CreditoContaCorrente(fakeMediator.Object);

            Equals(contaCorrente.saldoContaCorrente == 10000);
        }




        [Fact]
        public void TESTAR_DEBITO_CC()
        {

            var fakeMediator = new Mock<IContaCorrenteCommand>();

            fakeMediator.Setup(m => m.numeroContaOrigem).Returns(12346565);
            fakeMediator.Setup(m => m.numeroContaDestino).Returns(123456749);
            fakeMediator.Setup(m => m.valorLancamento).Returns(500);


            var contaCorrente = new ContaCorrente.Domain.Entities.ContaCorrente
            {
                numeroConta = 123456749,
                saldoContaCorrente = 5000,
                lancamentos = new List<Lancamentos> { new Lancamentos(TipoLancamento.Debito, 5000) }
            };

            contaCorrente.DebitoContaCorrente(fakeMediator.Object);

            Equals(contaCorrente.saldoContaCorrente == 4500);
        }
    }
}
