using Domain.Core.Notifications;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Service;
using Infrastructure.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Services.Services
{
    public class HistoricoCorridaService : BaseService<HistoricoCorrida>
    {
        public HistoricoCorridaService(IBaseRepository<HistoricoCorrida> baseRepository, INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IUnitOfWork unitOfWork, DataContext bancoContexto) : base(baseRepository, notifications, mediator, unitOfWork, bancoContexto)
        {
        }

        private IQueryable<HistoricoCorrida> DefaultQuery()
        {
            return _contexto.HistoricoCorrida
                            .Where(x => x.Id != 0)
                            .AsNoTracking()
                            .Include(x => x.PistaCorrida)
                            .Include(x => x.Competidor);
        }

        private IQueryable<HistoricoCorrida> ObterTodos()
        {
            return DefaultQuery().OrderBy(x => x.DataCorrida);
        }
    }
}
