using System;

namespace Cpnucleo.Domain.Models
{
    public class RecursoTarefa : BaseModel
    {
        public int PercentualTarefa { get; set; }

        public Guid IdRecurso { get; set; }

        public Guid IdTarefa { get; set; }

        public Recurso Recurso { get; set; }

        public Tarefa Tarefa { get; set; }
    }
}