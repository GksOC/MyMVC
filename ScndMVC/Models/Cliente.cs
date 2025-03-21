using System.Collections.Generic;
using System;
using System.Linq;

namespace ScndMVC.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public string Endereco { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DateModificacao { get; set; }
        public int IdUsuarioCriacao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }



        public Cliente()
        {

        }

        //construtor para o serviço seedingService
        public Cliente(int iD, string nome, string telefone, string email, string empresa, string endereco)
        {
            ID = iD;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Empresa = empresa;
            Endereco = endereco;
            DataCriacao = DateTime.Now;
            IdUsuarioCriacao = 0;
        }
    }
}
