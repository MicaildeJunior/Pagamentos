using Pagamentos.PaymentContext.Shared.Commands;

namespace Pagamentos.PaymentContext.Domain.Commands;

public class CommandResult : ICommandResult
{
    public CommandResult()
    {
        
    }
    public CommandResult(bool succes, string message)
    {
        Succes = succes;
        Message = message;
    }

    public bool Succes { get; set; }
    public string Message { get; set; }
}
