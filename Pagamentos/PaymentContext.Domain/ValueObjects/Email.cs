using Pagamentos.PaymentContext.Shared.ValueObjects;

namespace Pagamentos.PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;
    }

    public string Address { get; set; }
}
