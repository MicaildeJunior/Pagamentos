using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagamentos.PaymentContext.Domain.Commands;
using Pagamentos.PaymentContext.Domain.Entities;
using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Domain.Handlers;
using Pagamentos.PaymentContext.Domain.ValueObjects;
using Pagamentos.PaymentContext.Tests.FakeRepository;
using Pagamentos.PaymentContext.Tests.Mocks;

namespace Pagamentos.PaymentContext.Tests.Handlers;

public class SubscriptionHandlerTests
{
    // Red, Green, Refactor

    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());

        var command = new CreateBoletoSubscriptionCommand();
        command.FirstName = "Peter";
        command.LastName = "Parker";
        command.Document = "00000000000";
        command.Email = "peter.parker@gmail.com";

        command.BarCode = "123456789";
        command.BoletoNumber = "12345678901";

        command.PaymentNumber = "01010101";
        command.PaidDate = DateTime.Now;
        command.ExpireDate = DateTime.Now.AddMonths(1);
        command.Total = 60;
        command.TotalPaid = 60;
        command.Payer = "Parker Corp";
        command.PayerDocument = "123456788900";
        command.PayerDocumentType = EDocumentType.CPF;
        command.PayerEmail = "parker_corp@gmail.com";

        command.Street = "rua qqqqq";
        command.Number = "12345";
        command.Neighborhood = "iiiiiii";
        command.City = "ny";
        command.State = "zZzZz";
        command.Country = "USA";
        command.Zipode = "7777";

        handler.Handle(command);
        Assert.AreEqual(false, command.Valid);
    }
}