using Aplication.Controllers.Base;
using Aplication.ViewModels;
using AutoMapper;
using Domain.Core.Notifications;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Validators;
using System.Collections.Generic;

namespace Aplication.Controllers
{
    [ApiController]
    [Route("competidores")]
    public class CompetidoresController : BaseController
    {
        private IBaseService<Competidores> _competidorService;
        private IMapper _mapper;
        public CompetidoresController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, IBaseService<Competidores> competidorService) : base(notifications, mediator, mapper)
        {
            _mapper = mapper;
            _competidorService = competidorService;
        }

        /// <summary>
        /// Lista todos os Competidores cadastrados.
        /// </summary>
        /// <returns>Retorna competidores cadastrados</returns>
        /// <response code="200">Retorna competidores cadastrados</response>
        [HttpGet]
        [Route("get-all")]
        public ActionResult GetAll()
        {
            return Ok(_competidorService.GetAll());
        }

        /// <summary>
        /// Cadastra novos Competidores.
        /// </summary>
        /// <returns>Retorna último competidor cadastrado</returns>
        /// <response code="200">Retorna último competidor cadastrado, com variavél sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("create-new")]
        public ActionResult Create(CompetidoresViewModel competidor)
        {
            var nCompetidor = _mapper.Map<Competidores>(competidor);

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            var result = _competidorService.Add<CompetidorValidator>(nCompetidor);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }

        /// <summary>
        /// Cadastra novos Competidores.
        /// </summary>
        /// <returns>Retorna último competidor atualizado</returns>
        /// <response code="200">Retorna o competidor informado, com os dados atualizados e variavel de sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("update/{id}")]
        public ActionResult Update(int id)
        {
            var competidor = _competidorService.GetById(id);

            if (competidor == null)
                return BadRequest("Não foi possível localizar o Competidor informado.");

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            var result = _competidorService.Add<CompetidorValidator>(competidor);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }
    }
}
