using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScndMVC.Models;
using ScndMVC.Models.Enums;

namespace ScndMVC.Data
{
    public class SeedingService
    {
        private MainContext _context;

        public SeedingService(MainContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            Console.WriteLine("Entrou no Seeding Service!");

            if (!(_context.Agendamento.Any() ||
               _context.Configuracao.Any() ||
               _context.Funcionario.Any() ||
               _context.Servico.Any()))
            {
                Funcionario administrador = new Funcionario();
                administrador.ID = 1;
                administrador.NmProfissional = "Administrador";
                administrador.Telefone = "31995494229";
                administrador.Email = "";
                administrador.Login = "admin";
                administrador.Senha = "raasch@2025";
                administrador.Administrador = true;
                administrador.adicionarCriador(administrador.ID);

                _context.Funcionario.Add(administrador);


                Configuracao c1 = new Configuracao(1, administrador, false, false, false, false, false, false, false, 30,
                                                   new TimeSpan(7, 0, 0), new TimeSpan(18, 0, 0), new TimeSpan(12, 0, 0), new TimeSpan(14, 0, 0),
                                                   false);
                _context.Configuracao.Add(c1);

                Funcionario f1 = new Funcionario(2, administrador, "Funcionario exemplo", "3138242099", "RaaschCabeleleiroUnissex@gmail.com", "cobaia", "teste123", false, c1);
                f1.Configuracao = c1;
                _context.Funcionario.Add(f1);

                Servico so1 = new Servico(1, administrador, f1, "Exemplo", "Este serviço é apenas um exemplo para mostrar como o aplicativo funciona", 10f);
                _context.Servico.Add(so1);

                Agendamento a1 = new Agendamento(1, administrador, f1, new DateTime(2025, 4, 30), new TimeSpan(8, 0, 0), "Fulano", so1, null, Status.Agendado);
                Agendamento a2 = new Agendamento(2, administrador, f1, new DateTime(2025, 4, 30), new TimeSpan(7, 30, 0), "Ciclano", so1, 30f, Status.Realizado);
                Agendamento a3 = new Agendamento(3, administrador, f1, new DateTime(2025, 4, 30), new TimeSpan(7, 0, 0), "Beltrano", so1, null, Status.Cancelado);
                _context.Agendamento.AddRange(a1, a2, a3);

                Console.WriteLine("O Seeding service preencheu o banco!");
            }

            _context.SaveChanges();
            Console.WriteLine("Seeding service executado!");
        }
    }
}
