using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagamentos.PaymentContext.Domain.Commands;
using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Domain.ValueObjects;

namespace Pagamentos.PaymentContext.Tests.Entities;

[TestClass]
public class CreateBoletoSubscriptionCommandTests
{
    // Red, Green, Refactor

    [TestMethod]
    public void ShouldReturnErrorWhenNameIsInvalid()
    {
        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "";

        command.Validate();
        Assert.AreEqual(false, command.Valid);
    }
}
