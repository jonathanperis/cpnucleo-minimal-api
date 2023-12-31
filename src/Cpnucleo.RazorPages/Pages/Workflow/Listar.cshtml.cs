﻿using Cpnucleo.Shared.Queries.ListWorkflow;

namespace Cpnucleo.RazorPages.Pages.Workflow;

[Authorize]
public class ListarModel : PageModel
{
    private readonly ICpnucleoApiClient _cpnucleoApiClient;

    public ListarModel(ICpnucleoApiClient cpnucleoApiClient)
    {
        _cpnucleoApiClient = cpnucleoApiClient;
    }

    public WorkflowDto Workflow { get; set; }

    public IEnumerable<WorkflowDto> Lista { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        try
        {
            var result = await _cpnucleoApiClient.ExecuteAsync<ListWorkflowViewModel>("Workflow", "ListWorkflow", new ListWorkflowQuery());

            if (result.OperationResult == OperationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Não foi possível processar a solicitação no momento.");
                return Page();
            }

            Lista = result.Workflows;

            return Page();
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return Page();
        }
    }
}
