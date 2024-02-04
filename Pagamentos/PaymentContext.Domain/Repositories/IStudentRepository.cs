using Pagamentos.PaymentContext.Domain.Entities;

namespace Pagamentos.PaymentContext.Domain.Repositories;

public interface IStudentRepository
{
    bool DocumentExists(string document);
    bool EmailExists(string email);
    void CreateSbuscription(Student student);
}
