﻿using Cpnucleo.API.Filters;
using Cpnucleo.Application.Interfaces;
using Cpnucleo.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Cpnucleo.API.Controllers.V2
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2")]
    [ServiceFilter(typeof(AuthorizerActionFilter), Order = 1)]
    public class WorkflowController : ControllerBase
    {
        private readonly IWorkflowAppService _workflowAppService;

        public WorkflowController(IWorkflowAppService workflowAppService)
        {
            _workflowAppService = workflowAppService;
        }

        /// <summary>
        /// Listar workflows
        /// </summary>
        /// <remarks>
        /// # Listar workflows
        /// 
        /// Lista workflows da base de dados.
        /// </remarks>
        /// <response code="200">Retorna uma lista de workflows</response>
        [HttpGet]
        [ProducesResponseType(200)]
        public IEnumerable<WorkflowViewModel> Get()
        {
            return _workflowAppService.Listar();
        }

        /// <summary>
        /// Listar workflows por tarefa
        /// </summary>
        /// <remarks>
        /// # Listar workflows por tarefa
        /// 
        /// Lista workflows por tarefa na base de dados.
        /// </remarks>
        /// <response code="200">Retorna uma lista de workflows</response>
        [HttpGet("GetByTarefa")]
        [ProducesResponseType(200)]
        public IEnumerable<WorkflowViewModel> GetByTarefa()
        {
            return _workflowAppService.ListarPorTarefa();
        }

        /// <summary>
        /// Consultar workflow
        /// </summary>
        /// <remarks>
        /// # Consultar workflow
        /// 
        /// Consulta um workflow na base de dados.
        /// </remarks>
        /// <param name="id">Id do workflow</param>        
        /// <response code="200">Retorna um workflow</response>
        /// <response code="404">Workflow não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public ActionResult<WorkflowViewModel> Get(Guid id)
        {
            WorkflowViewModel workflow = _workflowAppService.Consultar(id);

            if (workflow == null)
            {
                return NotFound();
            }

            return Ok(workflow);
        }

        /// <summary>
        /// Incluir workflow
        /// </summary>
        /// <remarks>
        /// # Incluir workflow
        /// 
        /// Inclui um workflow na base de dados.
        /// 
        /// # Sample request:
        ///
        ///     POST /workflow
        ///     {
        ///        "nome": "Novo workflow",
        ///        "ordem": "3"
        ///     }
        /// </remarks>
        /// <param name="obj">Workflow</param>        
        /// <response code="201">Workflow cadastrado com sucesso</response>
        /// <response code="400">Objetos não preenchidos corretamente</response>
        /// <response code="409">Guid informado já consta na base de dados</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        public ActionResult<WorkflowViewModel> Post([FromBody]WorkflowViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _workflowAppService.Incluir(obj);
            }
            catch (DbUpdateException)
            {
                if (ObjExists(obj.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(Get), new { id = obj.Id }, obj);
        }

        /// <summary>
        /// Alterar workflow
        /// </summary>
        /// <remarks>
        /// # Alterar workflow
        /// 
        /// Altera um workflow na base de dados.
        /// 
        /// # Sample request:
        ///
        ///     PUT /workflow
        ///     {
        ///        "id": "fffc0a28-b9e9-4ffd-0053-08d73d64fb91",
        ///        "nome": "Novo workflow - alterado",
        ///        "ordem": "3,
        ///        "dataInclusao": "2019-09-21T19:15:23.519Z"
        ///     }
        /// </remarks>
        /// <param name="id">Id do workflow</param>        
        /// <param name="obj">Workflow</param>        
        /// <response code="204">Workflow alterado com sucesso</response>
        /// <response code="400">ID informado não é válido</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Put(Guid id, [FromBody]WorkflowViewModel obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj.Id)
            {
                return BadRequest();
            }

            try
            {
                _workflowAppService.Alterar(obj);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ObjExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Remover workflow
        /// </summary>
        /// <remarks>
        /// # Remover workflow
        /// 
        /// Remove um workflow da base de dados.
        /// </remarks>
        /// <param name="id">Id do workflow</param>        
        /// <response code="204">Workflow removido com sucesso</response>
        /// <response code="404">Workflow não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(Guid id)
        {
            WorkflowViewModel obj = _workflowAppService.Consultar(id);

            if (obj == null)
            {
                return NotFound();
            }

            _workflowAppService.Remover(id);

            return NoContent();
        }

        private bool ObjExists(Guid id)
        {
            return _workflowAppService.Consultar(id) != null;
        }
    }
}