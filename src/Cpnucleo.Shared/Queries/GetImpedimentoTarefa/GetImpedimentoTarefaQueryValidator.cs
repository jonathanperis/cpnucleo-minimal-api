﻿namespace Cpnucleo.Shared.Queries.GetImpedimentoTarefa;

public sealed class GetImpedimentoTarefaQueryValidator : AbstractValidator<GetImpedimentoTarefaQuery>
{
    public GetImpedimentoTarefaQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Necessário informar o Id do Impedimento Tarefa");
    }
}
