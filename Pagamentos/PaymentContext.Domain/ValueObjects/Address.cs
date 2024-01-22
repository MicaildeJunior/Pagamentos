using Flunt.Validations;
using Pagamentos.PaymentContext.Shared.ValueObjects;

namespace Pagamentos.PaymentContext.Domain.ValueObjects;

public class Address : ValueObject
{
    public Address(string street, string number, string neighborhood, string city, string state, string country, string zipode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        Zipode = zipode;

        AddNotifications(new Contract()
           .Requires()
           .HasMinLen(Street, 3, "Adress.Street", "A rua deve conter pelo menos 3 caracteres")           
        );
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private  set; }
    public string Zipode { get; set; }
}
