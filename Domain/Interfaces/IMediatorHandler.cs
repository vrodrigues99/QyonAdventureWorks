using MediatR;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : INotification;
        Task EnviarComando<T>(T comando) where T : IRequest;
    }
}
