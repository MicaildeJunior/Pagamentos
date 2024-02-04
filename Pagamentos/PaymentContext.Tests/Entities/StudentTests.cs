using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagamentos.PaymentContext.Domain.Entities;
using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Domain.ValueObjects;

namespace Pagamentos.PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Email _email;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Peter", "Parker");
        _document = new Document("11496640020", EDocumentType.CPF);
        _email = new Email("peter_parker@gmail.com");
        _address = new Address("Rua Ingram", "20", "Bairro Forest Hill Gardens", "Queens", "NY", "USA", "11375");
        _student = new Student(_name, _document, _email);
        // Passar nulo por que é uma assinatura recorrente
        var _subscription = new Subscription(null);        
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Parker Corp", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);       
        _student.AddSubscription(_subscription); // essa é pra dar erro        
        
        Assert.IsTrue(_student.Invalid); // se der tudo certo no codigo acima vai retornar Invalid
    }

    [TestMethod]
    public void ShouldReturnErrorWhenHadSubscriptionHasNoPayment()
    {
        var name = new Name("Peter", "Parker");
        var document = new Document("11496640020", EDocumentType.CPF);
        var email = new Email("peter_parker@gmail.com");
        var student = new Student(name, document, email);

        Assert.Fail();
    }

    [TestMethod]
    public void ShoulReturnSuccessWhenHadNoAddSubscription()
    {
        var payment = new PayPalPayment("12345678", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Parker Corp", _document, _address, _email);
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);       
        
        Assert.IsTrue(_student.Valid); 
    }
}