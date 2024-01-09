using Pagamentos.PaymentContext.Shared.ValueObjects;

namespace Pagamentos.PaymentContext.Domain.ValueObjects;

public class Address : ValueObject
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private  set; }
    public string Zipode { get; set; }
}
