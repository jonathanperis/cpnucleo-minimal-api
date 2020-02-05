﻿using Cpnucleo.Infra.CrossCutting.Communication.GRPC.Interfaces;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Cpnucleo.RazorPages.GRPC.Pages.Projeto
{
    [Authorize]
    public class AlterarModel : PageModel
    {
        private readonly IProjetoGrpcService _projetoGrpcService;
        private readonly ISistemaGrpcService _sistemaGrpcService;

        public AlterarModel(IProjetoGrpcService projetoGrpcService,
                            ISistemaGrpcService sistemaGrpcService)
        {
            _projetoGrpcService = projetoGrpcService;
            _sistemaGrpcService = sistemaGrpcService;
        }

        [BindProperty]
        public ProjetoViewModel Projeto { get; set; }

        public SelectList SelectSistemas { get; set; }

        public async Task<IActionResult> OnGet(Guid id)
        {
            Projeto = await _projetoGrpcService.ConsultarAsync(id);
            SelectSistemas = new SelectList(await _sistemaGrpcService.ListarAsync(), "Id", "Nome");

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                SelectSistemas = new SelectList(await _sistemaGrpcService.ListarAsync(), "Id", "Nome");

                return Page();
            }

            await _projetoGrpcService.AlterarAsync(Projeto);

            return RedirectToPage("Listar");
        }
    }
}