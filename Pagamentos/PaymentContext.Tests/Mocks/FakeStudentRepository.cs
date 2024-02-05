using Pagamentos.PaymentContext.Domain.Entities;
using Pagamentos.PaymentContext.Domain.Repositories;

namespace Pagamentos.PaymentContext.Tests.Mocks;

public class FakeStudentRepository : IStudentRepository
{
    public void CreateSbuscription(Student student)
    {        
    }

    public bool DocumentExists(string document)
    {
        if (document == "00000000000")
            return true;

        return false;
    }

    public bool EmailExists(string email)
    {
        if (email == "peter.parker@gmail.com")
            return true;   

        return false;
    }
}