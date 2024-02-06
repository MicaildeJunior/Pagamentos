using Pagamentos.PaymentContext.Domain.Entities;
using System.Linq.Expressions;

namespace Pagamentos.PaymentContext.Domain.Queries;

// Classe static pq não iremos instanciar nada aqui
public static class StudentQueries
{
    public static Expression<Func<Student, bool>> GetStudentInfo(string document)
    {
        return x => x.Document.Number == document;
    }
}