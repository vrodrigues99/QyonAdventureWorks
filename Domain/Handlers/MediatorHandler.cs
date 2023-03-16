using Domain.Interfaces;
using MediatR;
using System.Threading.Tasks;

namespace Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task EnviarComando<T>(T comando) where T : IRequest
        {
            await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : INotification
        {
            await _mediator.Publish(evento);
        }
    }
}
