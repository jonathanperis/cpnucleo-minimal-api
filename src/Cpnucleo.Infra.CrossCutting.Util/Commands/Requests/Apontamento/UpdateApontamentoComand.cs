﻿using Cpnucleo.Infra.CrossCutting.Util.Commands.Responses.Apontamento;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using MediatR;

namespace Cpnucleo.Infra.CrossCutting.Util.Commands.Requests.Apontamento
{
    public class UpdateApontamentoComand : IRequest<UpdateApontamentoResponse>
    {
        public ApontamentoViewModel Apontamento { get; set; }
    }
}