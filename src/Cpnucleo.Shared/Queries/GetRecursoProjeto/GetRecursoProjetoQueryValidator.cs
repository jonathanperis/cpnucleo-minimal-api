﻿namespace Cpnucleo.Shared.Queries.GetRecursoProjeto;

public sealed class GetRecursoProjetoQueryValidator : AbstractValidator<GetRecursoProjetoQuery>
{
    public GetRecursoProjetoQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Necessário informar o Id do Recurso Projeto");
    }
}
