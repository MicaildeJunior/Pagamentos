using Flunt.Notifications;
using Pagamentos.PaymentContext.Domain.Comands;
using Pagamentos.PaymentContext.Domain.Commands;
using Pagamentos.PaymentContext.Domain.Entities;
using Pagamentos.PaymentContext.Domain.Enums;
using Pagamentos.PaymentContext.Domain.Repositories;
using Pagamentos.PaymentContext.Domain.Services;
using Pagamentos.PaymentContext.Domain.ValueObjects;
using Pagamentos.PaymentContext.Shared.Commands;
using Pagamentos.PaymentContext.Shared.Handlers;

namespace Pagamentos.PaymentContext.Domain.Handlers;

public class SubscriptionHandler : 
    Notifiable, 
    IHandler<CreateBoletoSubscriptionCommand>,
    IHandler<CreatePayPalSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        // Fail Fast Validations
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assinatura");
        }
           
        // Verificar se o Documento já está cadastrado
        if (_repository.DocumentExists(command.Document)) 
            AddNotification("Document", "Este CPF já está em uso");

        // Verificar se o E-Mailstá cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        // Gerar Vos
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zipode);

        // Gerar as Entidades
        var student = new Student(name, document, email);

        // Passar nulo por que é uma assinatura recorrente
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(
            command.BarCode,
            command.BoletoNumber,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument, command.PayerDocumentType),
            address, 
            email
        );

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        // Gerar as notificações
        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar sua assinatura");



        // Salvar as Informções
        _repository.CreateSbuscription(student);

        // Enviar E-Mail de boas Vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Balta.io", "Sua assinatura foi criada");

        // Retornar informações
        return new CommandResult(true, "Assinatura realizada com sucesso");
    }

    public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
    {
        // Verificar se o Documento já está cadastrado
        if (_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");

        // Verificar se o E-Mailstá cadastrado
        if (_repository.EmailExists(command.Email))
            AddNotification("Email", "Este E-mail já está em uso");

        // Gerar Vos
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zipode);

        // Gerar as Entidades
        var student = new Student(name, document, email);

        // Passar nulo por que é uma assinatura recorrente
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        // Só muda a implementação do Pagamento
        var payment = new PayPalPayment(
            command.TransactionCode,
            command.PaidDate,
            command.ExpireDate,
            command.Total,
            command.TotalPaid,
            command.Payer,
            new Document(command.PayerDocument, command.PayerDocumentType),
            address,
            email
        );

        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);

        // Agrupar as Validações
        AddNotifications(name, document, email, address, student, subscription, payment);

        // Salvar as Informções
        _repository.CreateSbuscription(student);

        // Enviar E-Mail de boas Vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Balta.io", "Sua assinatura foi criada");
        // Retornar Informações
        return new CommandResult(true, "Seu pagamento foi realizado");
    }
}