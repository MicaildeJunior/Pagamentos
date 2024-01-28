using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Domain.ValueObjects;

namespace Pagamentos.PaymentContext.Tests.Entities;

[TestClass]
public class DocumentTests
{
    // Red, Green, Refactor

    [TestMethod]
    public void ShouldReturnErrorWhenCNPJIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CNPJ);
        Assert.IsTrue(doc.Invalid);
    }

    [TestMethod]
    public void ShouldReturnSuccessWhenCNPJIsValid()
    {
        var doc = new Document("34110468000150", EDocumentType.CNPJ);
        Assert.IsTrue(doc.Valid);
    }

    [TestMethod]
    public void ShouldReturnErrorWhenCPFIsInvalid()
    {
        var doc = new Document("123", EDocumentType.CPF);
        Assert.IsTrue(doc.Invalid);
    }

    [TestMethod]
    [DataTestMethod]
    [DataRow("12305806035")]
    [DataRow("68566516001")]
    [DataRow("64762411094")]
    public void ShouldReturnErrorWhenCPFIsValid(string cpf)
    {
        var doc = new Document(cpf, EDocumentType.CPF);
        Assert.IsTrue(doc.Valid);
    }
}
