﻿namespace Cpnucleo.Shared.Queries.ListImpedimentoTarefaByTarefa;

public sealed class ListImpedimentoTarefaByTarefaQueryValidator : AbstractValidator<ListImpedimentoTarefaByTarefaQuery>
{
    public ListImpedimentoTarefaByTarefaQueryValidator()
    {
        RuleFor(x => x.IdTarefa)
            .NotEmpty()
            .WithMessage("Impedimento Tarefa deve conter uma Tarefa");
    }
}
