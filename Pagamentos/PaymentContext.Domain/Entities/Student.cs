using Pagamentos.PaymentContext.Domain.ValueObjects;
using Pagamentos.PaymentContext.Shared.Entities;
using Flunt.Validations;

namespace Pagamentos.PaymentContext.Domain.Entities;

public class Student : Entity
{
    private IList<Subscription> _subscriptions;
    public Student(Name name, Document document, Email email)
    {
        Name = name;
        Document = document;
        Email = email;
        _subscriptions = new List<Subscription>();

        AddNotifications(name, document, email);
    }

    public Name Name { get; set; }
    public Document Document { get; private set; }
    public Email Email { get; private set; }
    public Address Address { get; private set; }
    public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

    public void AddSubscription(Subscription subscription)
    {
        var hasSubscriptionActive = false;
        foreach (var sub in _subscriptions)
        {
            if (subscription.Active)
                hasSubscriptionActive = true;
        }

        // Usando o Contrato
        // Essa alternativa é pra ter mais de uma implementação
        AddNotifications(new Contract()
            .Requires()
            .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura")
            // val <= comparer, Se o valor 0 for <= que assinatura.Pagamentos.Contador, tras a mensagem de erro
            // Não pode adicionar uma assinatura sem pagamento
            .IsGreaterThan(0, subscription.Payments.Count, "Student.Subscription.Payments", "Esta assinatura não possui pagamentos") 
        );

        // Outra Alternativa
        //if (hasSubscriptionActive)
        //    AddNotification("Student.Subscriptions", "Você já tem uma assinatura");
    }
}
