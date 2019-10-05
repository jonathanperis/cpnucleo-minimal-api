using System;
using System.ComponentModel.DataAnnotations;

namespace Cpnucleo.Infra.CrossCutting.Util.ViewModels
{
    public class RecursoTarefaViewModel : BaseViewModel
    {
        [Display(Name = "Percentual")]
        [Required(ErrorMessage = "Necessário informar o {0}.")]
        [Range(1, 100, ErrorMessage = "{0} deve estar entre {1} e {2}.")]
        public int? PercentualTarefa { get; set; }

        [Display(Name = "Recurso")]
        [Required(ErrorMessage = "Necessário informar o {0}.")]
        public Guid IdRecurso { get; set; }

        [Display(Name = "Tarefa")]
        [Required(ErrorMessage = "Necessário informar o {0}.")]
        public Guid IdTarefa { get; set; }

        public RecursoViewModel Recurso { get; set; }

        public TarefaViewModel Tarefa { get; set; }

        public int HorasUtilizadas { get; set; }

        public int HorasDisponiveis { get; set; }
    }
}