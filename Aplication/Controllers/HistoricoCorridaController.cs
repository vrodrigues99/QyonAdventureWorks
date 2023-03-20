using Aplication.Controllers.Base;
using Aplication.ViewModels;
using AutoMapper;
using Domain.Core.Notifications;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.Validators;
using System;
using System.Collections.Generic;

namespace Aplication.Controllers
{
    [ApiController]
    [Route("historico")]
    public class HistoricoCorridaController : BaseController
    {
        private HistoricoCorridaService _historicoService;
        private IMapper _mapper;

        public HistoricoCorridaController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, HistoricoCorridaService historicoService) : base(notifications, mediator, mapper)
        {
            _historicoService = historicoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get-all")]
        public ActionResult GetAll()
        {
            return Ok(_historicoService.ObterTodos());
        }

        /// <summary>
        /// Cadastra novo historico.
        /// </summary>
        /// <returns>Retorna último historico cadastrado</returns>
        /// <response code="200">Retorna último historico cadastrado, com variavél sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("include")]
        public ActionResult Include(HistoricoViewModel historico)
        {
            historico.DataCorrida = DateTime.Now.ToString();

            var nHistorico = _mapper.Map<HistoricoCorrida>(historico);

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            var result = _historicoService.Add<HistoricoCorridaValidator>(nHistorico);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }

        /// <summary>
        /// Atualiza historico.
        /// </summary>
        /// <returns>Retorna último historico atualizado</returns>
        /// <response code="200">Retorna último historico atualizado, com variavél sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("update/{id}")]
        public ActionResult Update(int id, HistoricoViewModel historico)
        {
            var uHistorico = _mapper.Map<HistoricoCorrida>(historico);

            if (uHistorico == null)
                return BadRequest("Não foi possível localizar o histórico informado.");

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            uHistorico.Id = id;

            var result = _historicoService.Update<HistoricoCorridaValidator>(uHistorico);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }
    }
}
