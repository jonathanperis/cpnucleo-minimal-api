﻿using AutoMapper;
using Cpnucleo.Application.Interfaces;
using Cpnucleo.Infra.CrossCutting.Communication.GRPC.Protos;
using Cpnucleo.Infra.CrossCutting.Util.ViewModels;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cpnucleo.GRPC
{
    public class TarefaService : Tarefa.TarefaBase
    {
        private readonly IMapper _mapper;
        private readonly ITarefaAppService _tarefaAppService;

        public TarefaService(IMapper mapper, ITarefaAppService tarefaAppService)
        {
            _mapper = mapper;
            _tarefaAppService = tarefaAppService;
        }

        public override async Task<BaseReply> Incluir(TarefaModel request, ServerCallContext context)
        {
            return await Task.FromResult(new BaseReply
            {
                Sucesso = _tarefaAppService.Incluir(_mapper.Map<TarefaViewModel>(request))
            });
        }

        public override async Task Listar(Empty request, IServerStreamWriter<TarefaModel> responseStream, ServerCallContext context)
        {
            foreach (TarefaModel item in _mapper.Map<IEnumerable<TarefaModel>>(_tarefaAppService.Listar()))
            {
                await responseStream.WriteAsync(item);
            }
        }

        public override async Task<TarefaModel> Consultar(BaseRequest request, ServerCallContext context)
        {
            Guid id = new Guid(request.Id);
            TarefaModel result = _mapper.Map<TarefaModel>(_tarefaAppService.Consultar(id));

            return await Task.FromResult(result);
        }

        public override async Task<BaseReply> Alterar(TarefaModel request, ServerCallContext context)
        {
            return await Task.FromResult(new BaseReply
            {
                Sucesso = _tarefaAppService.Alterar(_mapper.Map<TarefaViewModel>(request))
            });
        }

        public override async Task<BaseReply> Remover(BaseRequest request, ServerCallContext context)
        {
            return await Task.FromResult(new BaseReply
            {
                Sucesso = _tarefaAppService.Remover(new Guid(request.Id))
            });
        }
    }
}
