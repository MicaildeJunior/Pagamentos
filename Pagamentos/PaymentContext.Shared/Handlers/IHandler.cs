using Pagamentos.PaymentContext.Shared.Commands;

namespace Pagamentos.PaymentContext.Shared.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
