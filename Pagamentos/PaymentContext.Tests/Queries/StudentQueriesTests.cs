using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagamentos.PaymentContext.Domain.Entities;
using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Domain.Queries;
using Pagamentos.PaymentContext.Domain.ValueObjects;

namespace Pagamentos.PaymentContext.Tests.Handlers;

public class StudentQueriesTests
{
    // Red, Green, Refactor
    private IList<Student> _students;

    public StudentQueriesTests()
    {
        _students = new List<Student>();
        for (var i = 0; i <= 10; i++)
        {
            _students.Add(new Student(
                new Name("Aluno", i.ToString()),
                new Document("1111111111" + i.ToString(), EDocumentType.CPF),
                new Email(i.ToString() + "@peter.parker@gmail.com")
            ));
        }
    }

    [TestMethod]
    public void ShouldReturnNullWhenDocumentNotExists()
    {
        var exp = StudentQueries.GetStudentInfo("12345678901");
        var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreEqual(null, studn);
    }

    [TestMethod]
    public void ShouldReturnStudentWhenDocumentExists()
    {
        var exp = StudentQueries.GetStudentInfo("11111111111");
        var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

        Assert.AreEqual(null, studn);
    }
}