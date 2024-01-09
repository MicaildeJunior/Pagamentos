using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Shared.ValueObjects;

namespace Pagamentos.PaymentContext.Domain.ValueObjects;

public class Document : ValueObject
{
    public Document(string number, EDocumentType type)
    {
        Number = number;
        Type = type;
    }
    public string Number { get; private set; }
    public EDocumentType Type { get; set; }
}
