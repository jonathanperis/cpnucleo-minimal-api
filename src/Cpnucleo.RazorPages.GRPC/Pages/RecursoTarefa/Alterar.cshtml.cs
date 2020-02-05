﻿using Cpnucleo.Infra.CrossCutting.Communication.GRPC.Interfaces;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Cpnucleo.RazorPages.GRPC.Pages.RecursoTarefa
{
    [Authorize]
    public class AlterarModel : PageModel
    {
        private readonly IRecursoTarefaGrpcService _recursoTarefaGrpcService;
        private readonly IRecursoProjetoGrpcService _recursoProjetoGrpcService;
        private readonly ITarefaGrpcService _tarefaGrpcService;

        public AlterarModel(IRecursoTarefaGrpcService recursoTarefaGrpcService,
                                    IRecursoProjetoGrpcService recursoProjetoGrpcService,
                                    ITarefaGrpcService tarefaGrpcService)
        {
            _recursoTarefaGrpcService = recursoTarefaGrpcService;
            _recursoProjetoGrpcService = recursoProjetoGrpcService;
            _tarefaGrpcService = tarefaGrpcService;
        }

        [BindProperty]
        public RecursoTarefaViewModel RecursoTarefa { get; set; }

        public TarefaViewModel Tarefa { get; set; }

        public SelectList SelectRecursos { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            RecursoTarefa = await _recursoTarefaGrpcService.ConsultarAsync(id);
            SelectRecursos = new SelectList(await _recursoProjetoGrpcService.ListarPorProjetoAsync(RecursoTarefa.Tarefa.IdProjeto), "Recurso.Id", "Recurso.Nome");

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Tarefa = await _tarefaGrpcService.ConsultarAsync(RecursoTarefa.IdTarefa);
                SelectRecursos = new SelectList(await _recursoProjetoGrpcService.ListarPorProjetoAsync(Tarefa.IdProjeto), "Recurso.Id", "Recurso.Nome");

                return Page();
            }

            await _recursoTarefaGrpcService.AlterarAsync(RecursoTarefa);

            return RedirectToPage("Listar", new { idTarefa = RecursoTarefa.IdTarefa });
        }
    }
}