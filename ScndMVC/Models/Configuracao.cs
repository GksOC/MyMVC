using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ScndMVC.Models
{
    public class Configuracao : ResourceHistorico
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Domingo { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Segunda { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Terca { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Quarta { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Quinta { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Sexta { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        public bool Sabado { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [Range(10, 60, ErrorMessage = ("{0} deve ter um tamanho entre {1} a {2} (minutos)"))]
        [Display(Name = "Período de atendimento")]
        public int PeriodoAtendimento { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Início do expediente")]
        public TimeSpan HrInicio { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Fim do expediente")]
        public TimeSpan HrFim { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Início do intervalo")]
        public TimeSpan HrPausaInicio { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Fim do intervalo")]
        public TimeSpan HrPausaFim { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [Display(Name = "Agendamento com múltiplos períodos")]
        public bool AgendaMultipla {  get; set; }

        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Configuracao()
        {

        }

        public Configuracao(int iD, Funcionario criador, bool domingo, bool segunda, bool terca, bool quarta, bool quinta, bool sexta, bool sabado, 
                            int periodoAtendimento, TimeSpan hrInicio, TimeSpan hrFim, TimeSpan hrPausaInicio, TimeSpan hrPausaFim, bool agendaMultipla)
        {
            ID = iD;
            Domingo = domingo;
            Segunda = segunda;
            Terca = terca;
            Quarta = quarta;
            Quinta = quinta;
            Sexta = sexta;
            Sabado = sabado;
            PeriodoAtendimento = periodoAtendimento;
            HrInicio = hrInicio;
            HrFim = hrFim;
            HrPausaInicio = hrPausaInicio;
            HrPausaFim = hrPausaFim;
            AgendaMultipla = agendaMultipla;
            adicionarCriador(criador.ID);
        }

    }
}
