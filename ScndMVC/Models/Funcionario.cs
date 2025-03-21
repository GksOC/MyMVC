using System.Collections.Generic;
using System;
using System.Linq;

namespace ScndMVC.Models
{
    public class Funcionario
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int Funcao { get; set; } //atualizar para enum posteriormente

        public DateTime DataCriacao { get; set; }
        public DateTime? DateModificacao { get; set; }
        public int IdUsuarioCriacao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }



        public Funcionario()
        {

        }

        //construtor para o serviço seedingService
        public Funcionario(int iD, string nome, string telefone, string email, int funcao)
        {
            ID = iD;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Funcao = funcao;
            DataCriacao = DateTime.Now;
            IdUsuarioCriacao = 0;
        }
    }
}
