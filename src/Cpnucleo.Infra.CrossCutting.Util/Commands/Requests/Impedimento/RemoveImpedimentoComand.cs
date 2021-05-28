﻿using Cpnucleo.Infra.CrossCutting.Util.Commands.Responses.Impedimento;
using MediatR;
using System;

namespace Cpnucleo.Infra.CrossCutting.Util.Commands.Requests.Impedimento
{
    public class RemoveImpedimentoComand : IRequest<RemoveImpedimentoResponse>
    {
        public Guid Id { get; set; }
    }
}