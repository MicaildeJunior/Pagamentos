using Pagamentos.PaymentContext.Domain.Services;

namespace Pagamentos.PaymentContext.Tests.FakeRepository;

public class FakeEmailService : IEmailService
{
    public void Send(string to, string email, string subject, string body)
    {
       
    }
}
