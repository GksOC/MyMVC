using System.Collections.Generic;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace ScndMVC.Models
{
    public class Funcionario : ResourceHistorico
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [StringLength(63, MinimumLength = 3, ErrorMessage = "{0} deve ter um tamanho entre {2} e {1}")]
        [Display(Name = "Nome do profissional")]
        public string NmProfissional { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "{0} deve ter um tamanho entre {2} e {1}")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [EmailAddress(ErrorMessage = "Coloque um email válido!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} requerido!")]
        [StringLength(31, MinimumLength = 3, ErrorMessage = "{0} deve ter um tamanho entre {2} e {1}")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "{0} deve ter um tamanho entre {2} e {1}")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public Funcionario()
        {

        }

        public Funcionario(int iD, string nmProfissional, string telefone, string email, string login, string senha)
        {
            ID = iD;
            NmProfissional = nmProfissional;
            Telefone = telefone;
            Email = email;
            Login = login;
            Senha = senha;
        }

    }
}
