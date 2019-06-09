﻿using Cpnucleo.Pages.Models;
using Cpnucleo.Pages.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Cpnucleo.Pages.Pages.Projeto
{
    [Authorize]
    public class AlterarModel : PageModel
    {
        private readonly IRepository<ProjetoItem> _projetoRepository;

        private readonly IRepository<SistemaItem> _sistemaRepository;

        public AlterarModel(IRepository<ProjetoItem> projetoRepository, IRepository<SistemaItem> sistemaRepository)
        {
            _projetoRepository = projetoRepository;
            _sistemaRepository = sistemaRepository;
        }

        [BindProperty(SupportsGet = true)]
        public ProjetoItem Projeto { get; set; }

        public SelectList SelectSistemas { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Projeto = await _projetoRepository.ConsultarAsync(Projeto.IdProjeto);
            SelectSistemas = new SelectList(await _sistemaRepository.ListarAsync(), "IdSistema", "Nome");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SelectSistemas = new SelectList(await _sistemaRepository.ListarAsync(), "IdSistema", "Nome");

                return Page();
            }

            await _projetoRepository.AlterarAsync(Projeto);

            return RedirectToPage("Listar");
        }
    }
}