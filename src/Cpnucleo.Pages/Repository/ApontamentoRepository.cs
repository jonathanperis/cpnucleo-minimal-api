using Cpnucleo.Pages.Data;
using Cpnucleo.Pages.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cpnucleo.Pages.Repository
{
    public class ApontamentoRepository : IApontamentoRepository
    {
        private readonly Context _context;

        private readonly ITarefaRepository _tarefaRepository;

        public ApontamentoRepository(Context context,
                                    ITarefaRepository tarefaRepository)
        {
            _context = context;
            _tarefaRepository = tarefaRepository;
        }        

        public async Task IncluirAsync(ApontamentoItem apontamento)
        {           
            apontamento.DataInclusao = DateTime.Now;
            
            _context.Apontamentos.Add(apontamento);
            await _context.SaveChangesAsync();          
        }

        public Task AlterarAsync(ApontamentoItem apontamento)
        {
            throw new NotImplementedException();
        }

        public async Task<ApontamentoItem> ConsultarAsync(int idApontamento)
        {
            return await _context.Apontamentos
                .Include(x => x.Tarefa)
                .SingleOrDefaultAsync(x => x.IdApontamento == idApontamento);
        }

        public async Task<IEnumerable<ApontamentoItem>> ListarAsync()
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .OrderBy(x => x.DataInclusao)
                .ToListAsync();
        }  

        public async Task RemoverAsync(ApontamentoItem apontamento)
        {    
            var apontamentoItem = await ConsultarAsync(apontamento.IdApontamento);            

            _context.Apontamentos.Remove(apontamentoItem);
            await _context.SaveChangesAsync();
        }

        public async Task ApontarHorasAsync(ApontamentoItem apontamento)
        {
            await IncluirAsync(apontamento);

            var tarefaItem = await _tarefaRepository.ConsultarAsync(apontamento.IdTarefa);
            tarefaItem.PercentualConcluido = apontamento.PercentualConcluido;

            await _tarefaRepository.AlterarAsync(tarefaItem);
        }

        public async Task<int> ObterTotalHorasPoridRecursoAsync(int idRecurso, int idTarefa)
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .OrderBy(x => x.DataInclusao)
                .Where(x => x.IdRecurso == idRecurso && x.IdTarefa == idTarefa)
                .SumAsync(x => x.QtdHoras);
        }              

        public async Task<IEnumerable<ApontamentoItem>> ListarPoridRecursoAsync(int idRecurso)
        {
            return await _context.Apontamentos
                .AsNoTracking()
                .Include(x => x.Tarefa)
                .OrderBy(x => x.DataInclusao)
                .Where(x => x.IdRecurso == idRecurso && x.DataApontamento.Value > DateTime.Now.AddDays(-30))
                .ToListAsync();
        }                    
    }    
}