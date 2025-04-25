using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ScndMVC.Models
{
    public class Servico
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [Display(Name = "Funcionário")]
        public Funcionario IdFuncionario { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [StringLength(63, MinimumLength = 3, ErrorMessage = "{0} deve ter um tamanho entre {2} e {1}")]
        [Display(Name = "Nome do serviço")]
        public string NmServico { get; set; }

        [StringLength(63)]
        [Display(Name = "Descrição")]
        public string DsServico { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [Range(0.0f, 10000.0f, ErrorMessage = ("{0} deve ter um tamanho entre {1} e {2}"))]
        [Display(Name = "Valor padrão")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public float Valor { get; set; }

        public Servico()
        {

        }

        public Servico(int iD, Funcionario idFuncionario, string nmServico, string dsServico, float valor)
        {
            ID = iD;
            IdFuncionario = idFuncionario;
            NmServico = nmServico;
            DsServico = dsServico;
            Valor = valor;
        }

    }
}
