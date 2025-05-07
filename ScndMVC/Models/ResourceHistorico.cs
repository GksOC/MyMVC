using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace ScndMVC.Models
{
    public abstract class ResourceHistorico
    {

        [Required]
        [Display(Name = "Data de criação")]
        public DateTime DtCriacao { get; set; }

        [Display(Name = "Data de modificação")]
        public DateTime? DtModificao { get; set; }

        [Required]
        [Display(Name = "Criador")]
        public int IdCriador { get; set; }

        [Display(Name = "Autor modificacao")]
        public int? IdModificador { get; set; }


        public void adicionarCriador(int idCriador)
        {
            IdCriador = idCriador;
            DtCriacao = DateTime.Now;
        }

        public void atualizarModificao(int idFuncionario)
        {
            IdModificador = idFuncionario;
            DtModificao = DateTime.Now;
        }
    }
}
