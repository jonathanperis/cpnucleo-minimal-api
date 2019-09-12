﻿using AutoMapper;
using Cpnucleo.Application.Interfaces;
using Cpnucleo.Application.ViewModels;
using Cpnucleo.Domain.Interfaces;
using Cpnucleo.Domain.Models;
using System;
using System.Collections.Generic;

namespace Cpnucleo.Application.Services
{
    public class RecursoTarefaAppService : AppService<RecursoTarefa, RecursoTarefaViewModel>, IRecursoTarefaAppService
    {
        protected readonly IRecursoTarefaRepository _recursoTarefaRepository;
        protected readonly IApontamentoAppService _apontamentoAppService;

        public RecursoTarefaAppService(IMapper mapper, IRepository<RecursoTarefa> repository, IUnitOfWork unitOfWork, IRecursoTarefaRepository recursoTarefaRepository, IApontamentoAppService apontamentoAppService)
            : base(mapper, repository, unitOfWork)
        {
            _recursoTarefaRepository = recursoTarefaRepository;
            _apontamentoAppService = apontamentoAppService;
        }

        public IEnumerable<RecursoTarefaViewModel> ListarPoridRecurso(Guid idRecurso)
        {
            var listaRecursoTarefa = _mapper.Map<IEnumerable<RecursoTarefaViewModel>>(_recursoTarefaRepository.ListarPoridRecurso(idRecurso));

            foreach (var item in listaRecursoTarefa)
            {
                item.HorasUtilizadas = _apontamentoAppService.ObterTotalHorasPoridRecurso(item.IdRecurso, item.IdTarefa);

                if (item.PercentualTarefa != null)
                {
                    double horasFracionadas = ((item.Tarefa.QtdHoras / 100.0) * item.PercentualTarefa.Value);
                    item.HorasDisponiveis = (int)(horasFracionadas - item.HorasUtilizadas);
                }
            }

            return listaRecursoTarefa;
        }

        public IEnumerable<RecursoTarefaViewModel> ListarPoridTarefa(Guid idTarefa)
        {
            return _mapper.Map<IEnumerable<RecursoTarefaViewModel>>(_recursoTarefaRepository.ListarPoridTarefa(idTarefa));
        }
    }
}
