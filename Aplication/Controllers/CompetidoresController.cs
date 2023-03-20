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
using System.ComponentModel;
using System.Linq;
using System.Net;

namespace Aplication.Controllers
{
    [ApiController]
    [Route("competidores")]
    public class CompetidoresController : BaseController
    {
        private IBaseService<Competidores> _competidorService;
        private HistoricoCorridaService _historicoService;
        private IMapper _mapper;

        public CompetidoresController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator, IMapper mapper, IBaseService<Competidores> competidorService, HistoricoCorridaService historicoService) : base(notifications, mediator, mapper)
        {
            _mapper = mapper;
            _historicoService = historicoService;
            _competidorService = competidorService;
        }

        /// <summary>
        /// Lista todos os competidores cadastrados.
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
        /// Cadastra novos competidores.
        /// </summary>
        /// <returns>Retorna último competidor cadastrado</returns>
        /// <response code="200">Retorna último competidor cadastrado, com variavél sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("create")]
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
        /// Atualiza cadastro de competidores.
        /// </summary>
        /// <returns>Retorna último competidor atualizado</returns>
        /// <response code="200">Retorna o competidor informado, com os dados atualizados e variavel de sucesso sendo true</response>
        /// <response code="400">Retorna mensagens de validação, com variavél sucesso sendo false</response>
        [HttpPost]
        [Route("update/{id}")]
        public ActionResult Update(int id, CompetidoresViewModel competidor)
        {
            var uCompetidor = _mapper.Map<Competidores>(competidor);

            if (uCompetidor == null)
                return BadRequest("Não foi possível localizar o Competidor informado.");

            NotificarErroModelInvalida();
            if (_notifications.HasNotifications())
                return BadRequest();

            uCompetidor.Id = id;

            var result = _competidorService.Update<CompetidorValidator>(uCompetidor);

            if (!_notifications.HasNotifications())
                return Ok(result);
            else
                return BadRequest(_notifications.GetNotifications());
        }

        /// <summary>
        /// Exclui o competidor especificado.
        /// </summary>
        /// <returns>Retorna mensagem de sucesso ou falha</returns>
        /// <response code="200">Retorna mensagem de sucesso</response>
        /// <response code="400">Retorna mensagem de falhga</response>
        [HttpPost]
        [Route("delete/{id}")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest("Nenhum competidor encontrado.");

            _competidorService.Delete(id);

            var check = _competidorService.GetById(id);

            if(check == null)
                return Ok("Competidor excluido com sucesso.");
            else
                return BadRequest("Não foi possivel excluir competidor.");
        }

        /// <summary>
        /// Lista todos os competidores cadastrados com o tempo médio de cada um.
        /// </summary>
        /// <returns>Retorna competidores cadastrados com tempo médio</returns>
        /// <response code="200">Retorna competidores cadastrados com tempo médio</response>
        [HttpGet]
        [Route("tempo-medio")]
        public ActionResult TempoMedio()
        {
            var competidores = _competidorService.GetAll();

            var historicos = _historicoService.ObterTodos();

            var result = new List<TempoMedioViewModel>();

            foreach (var competidor in competidores)
            {
                var aux = new TempoMedioViewModel();

                aux.Competidor = _mapper.Map<CompetidoresViewModel>(competidor);

                foreach (var historico in historicos.Where(x => x.CompetidorId == competidor.Id))
                {

                    aux.TempoMedioGasto += historico.TempoGasto;
                }

                result.Add(aux);
            }

            return Ok(result);
        } 
    }
}
