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
        public DateTime DtModificao { get; set; }

        [Required]
        [Display(Name = "Criador")]
        public Funcionario UsuarioCriador { get; set; }

        [Required]
        [Display(Name = "Autor modificacao")]
        public Funcionario UsuarioModificacao { get; set; }


        public void adicionarCriador(Funcionario funcionario)
        {
            UsuarioCriador = funcionario;
            DtCriacao = DateTime.Now;
        }

        public void atualizarModificao(Funcionario funcionario)
        {
            UsuarioModificacao = funcionario;
            DtModificao = DateTime.Now;
        }
    }
}
