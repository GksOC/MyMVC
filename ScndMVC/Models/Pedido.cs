using System.Collections.Generic;
using System;
using System.Linq;

namespace ScndMVC.Models
{
    public class Pedido
    {
        public int ID { get; set; }
        public Cliente Cliente { get; set; }
        public Funcionario Responsavel { get; set; }
        public string DescPedido { get; set; }
        public float Preco { get; set; }
        public DateTime DataPedido { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DateModificacao { get; set; }
        public int IdUsuarioCriacao { get; set; }
        public int? IdUsuarioAlteracao { get; set; }



        public Pedido()
        {

        }

        //construtor para o serviço seedingService
        public Pedido(int iD, Cliente cliente, Funcionario responsavel, string descPedido, float preco, DateTime dataPedido)
        {
            ID = iD;
            Cliente = cliente;
            Responsavel = responsavel;
            DescPedido = descPedido;
            Preco = preco;
            DataPedido = dataPedido;
            DataCriacao = DateTime.Now;
            IdUsuarioCriacao = 0;
        }
    }
}
