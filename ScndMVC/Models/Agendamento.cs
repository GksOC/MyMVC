using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using ScndMVC.Models.Enums;

namespace ScndMVC.Models
{
    public class Agendamento : ResourceHistorico
    {
        public int ID { get; set; }

        [Required]
        public int FuncionarioID { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [Display(Name = "Dia")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DtDia { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Horário")]
        public TimeSpan HrAgendamento { get; set; }

        [StringLength(63, MinimumLength = 2, ErrorMessage = "{0} deve ter um tamanho entre {2} e {1}")]
        [Display(Name = "Cliente")]
        public string NmCliente { get; set; }

        [Display(Name = "Serviço")]
        public Servico Servico { get; set; }

        [Range(0.0f, 10000.0f, ErrorMessage = ("{0} deve ter um tamanho entre {1} e {2}"))]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public float? Valor { get; set; }

        [Required]
        [Display(Name = "Status")]
        public Status Stats { get; set; }


        public Agendamento()
        {

        }

        public Agendamento(int iD, Funcionario criador, Funcionario funcionario, DateTime dtDia, TimeSpan hrAgendamento, string nmCliente, Servico servico, float? valor, Status stats)
        {
            ID = iD;
            FuncionarioID = funcionario.ID;
            DtDia = dtDia;
            HrAgendamento = hrAgendamento;
            NmCliente = nmCliente;
            Servico = servico;
            Valor = valor;
            Stats = stats;
            adicionarCriador(criador.ID);
        }

    }
}
