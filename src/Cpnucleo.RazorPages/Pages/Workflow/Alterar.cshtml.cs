﻿using Cpnucleo.Shared.Commands.UpdateWorkflow;
using Cpnucleo.Shared.Queries.GetWorkflow;

namespace Cpnucleo.RazorPages.Pages.Workflow;

[Authorize]
public class AlterarModel : PageModel
{
    private readonly ICpnucleoApiClient _cpnucleoApiClient;

    public AlterarModel(ICpnucleoApiClient cpnucleoApiClient)
    {
        _cpnucleoApiClient = cpnucleoApiClient;
    }

    [BindProperty]
    public WorkflowDto Workflow { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        try
        {
            await CarregarDados(id);

            return Page();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (!ModelState.IsValid)
            {
                await CarregarDados(Workflow.Id);

                return Page();
            }

            var result = await _cpnucleoApiClient.ExecuteAsync<OperationResult>("Workflow", "UpdateWorkflow", new UpdateWorkflowCommand(Workflow.Id, Workflow.Nome, Workflow.Ordem));

            if (result == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return Page();
            }

            return RedirectToPage("Listar");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }

    private async Task CarregarDados(Guid idWorkflow)
    {
        var result = await _cpnucleoApiClient.ExecuteAsync<GetWorkflowViewModel>("Workflow", "GetWorkflow", new GetWorkflowQuery(idWorkflow));

        if (result.OperationResult == OperationResult.Failed)
        {
            ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
            return;
        }

        Workflow = result.Workflow;
    }
}
