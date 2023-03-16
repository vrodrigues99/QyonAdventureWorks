using Domain.Interfaces;
using Domain.Core.Notifications;
using MediatR;
using FluentValidation.Results;
using Domain.Interfaces.Service;
using Domain.Entities.Base;
using FluentValidation;
using System.Collections.Generic;
using System;
using Infrastructure.Data.Context;

namespace Services.Base
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        protected DataContext _contexto;
        protected IUnitOfWork _unitOfWork;
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;
        protected readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository,
                              INotificationHandler<DomainNotification> notifications,
                              IMediatorHandler mediator,
                              IUnitOfWork unitOfWork,
                              DataContext bancoContexto)
        {
            _baseRepository = baseRepository;
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _contexto = bancoContexto;
        }

        protected void NotificarValidacoesErro(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                _mediator.PublicarEvento(new DomainNotification(error.PropertyName, error.ErrorMessage));
        }

        protected bool OperacaoValida()
        {
            return (!_notifications.HasNotifications());
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediator.PublicarEvento(new DomainNotification(codigo, mensagem));
        }

        public OperationResult<TEntity> Add<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            if (!_notifications.HasNotifications())
            {
                _baseRepository.Add(obj);
                if (!_notifications.HasNotifications())
                    return new OperationResult<TEntity>(true, obj);
            }
            return new OperationResult<TEntity>(false, obj);
        }

        public OperationResult<TEntity> Update<TValidator>(TEntity obj) where TValidator : AbstractValidator<TEntity>
        {
            Validate(obj, Activator.CreateInstance<TValidator>());
            if (!_notifications.HasNotifications())
            {
                _baseRepository.Update(obj);
                if (!_notifications.HasNotifications())
                    return new OperationResult<TEntity>(true, obj);
            }
            return new OperationResult<TEntity>(false, obj);
        }

        public TEntity GetById(int id) => _baseRepository.GetById(id);

        public IEnumerable<TEntity> GetAll() => _baseRepository.GetAll();

        public void Delete(int id) => _baseRepository.Remove(id);

        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");

            ValidationResult result = validator.Validate(obj);

            if (!result.IsValid)
                foreach (var item in result.Errors)
                    NotificarErro(string.Empty, item.ErrorMessage);
        }
    }
}
