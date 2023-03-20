using Aplication.Controllers.Base;
using Aplication.ViewModels;
using AutoMapper;
using Domain.Core.Notifications;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using Services.Validators;
using System.Collections.Generic;

namespace Aplication.Controllers
{
    [ApiController]
    [Route("pistas")]
    public class PistasController : BaseController
    {
        private IBaseService<PistaCorrida> _pistaService;
        private HistoricoCorridaService _historicoService;
        private IMapper _mapper;

        public PistasController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, IBaseService<PistaCorrida> pistaService, HistoricoCorridaService historicoService) : base(notifications, mediator, mapper)
        {
            _pistaService = pistaService;
            _historicoService = historicoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Lista todos as pistas cadastradas.
        /// </summary>
        /// <returns>Retorna pistas cadastradas</returns>
        /// <response code="200">Retorna pistas cadastradas</response>
        [HttpGet]
        [Route("get-all")]
        public ActionResult GetAll()
        {
            return Ok(_pistaService.GetAll());
        }

        /// <summary>
        /// Cadastra nova pista.
        /// </summary>
        /// <returns>Retorna última pista cadastrada</returns>
        /// <response code="200">Retorna última pista cadastrada, com variavél sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("create")]
        public ActionResult Create(PistaViewModel pista)
        {
            var nPista = _mapper.Map<PistaCorrida>(pista);

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            var result = _pistaService.Add<PistaValidator>(nPista);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }

        /// <summary>
        /// Atualiza cadastro de pista.
        /// </summary>
        /// <returns>Retorna última pista atualizada</returns>
        /// <response code="200">Retorna a pista informada, com os dados atualizados e variavel de sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("update/{id}")]
        public ActionResult Update(int id, PistaViewModel pista)
        {
            var uPista = _mapper.Map<PistaCorrida>(pista);

            if (uPista == null)
                return BadRequest("Não foi possível localizar a pista informada.");

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            uPista.Id = id;

            var result = _pistaService.Update<PistaValidator>(uPista);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }

        /// <summary>
        /// Exclui a pista especificada.
        /// </summary>
        /// <returns>Retorna mensagem de sucesso ou falha</returns>
        /// <response code="200">Retorna mensagem de sucesso</response>
        /// <response code="400">Retorna mensagem de falhga</response>
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("Nenhuma pista encontrada.");

            _pistaService.Delete(id);

            var check = _pistaService.GetById(id);

            if (check == null)
                return Ok("Pista excluida com sucesso.");
            else
                return BadRequest("Não foi possivel excluir pista.");
        }

        /// <summary>
        /// Lista todos as pistas cadastradas utilizadas.
        /// </summary>
        /// <returns>Retorna pistas cadastradas utilizadas</returns>
        /// <response code="200">Retorna pistas cadastradas utilizadas</response>
        [HttpGet]
        [Route("pistas-utilizadas")]
        public ActionResult PistasUtilizadas()
        {
            var historico = _historicoService.ObterPistasUtilizadas();

            var pistas = new List<PistaCorrida>();

            foreach (var corrida in historico)
            {
                pistas.Add(corrida.PistaCorrida);
            }

            return Ok(pistas);
        }
    }
}
