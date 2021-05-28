﻿using Cpnucleo.Infra.CrossCutting.Util.Commands.Responses.TipoTarefa;
using MediatR;
using System;

namespace Cpnucleo.Infra.CrossCutting.Util.Commands.Requests.TipoTarefa
{
    public class RemoveTipoTarefaComand : IRequest<RemoveTipoTarefaResponse>
    {
        public Guid Id { get; set; }
    }
}