﻿namespace Cpnucleo.Shared.Queries.RecursoProjeto;

public record GetRecursoProjetoByProjetoQuery(Guid IdProjeto) : BaseQuery, IRequest<GetRecursoProjetoByProjetoViewModel>;