using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ScndMVC.Models.Services.Exceptions;

namespace ScndMVC.Models.Services
{
    public class FuncionarioService
    {
        private readonly MainContext _context;

        public FuncionarioService(MainContext context)
        {
            _context = context;
        }

        public async Task<List<Funcionario>> FindAllAsync()
        {
            return await _context.Funcionario.OrderBy(x => x.Login).ToListAsync();
        }

        public async Task<Funcionario> FindByIDAsync(int id)
        {
            return await _context.Funcionario.FirstOrDefaultAsync(obj => obj.ID == id);
        }

        public async Task UpdateAsync(Funcionario obj)
        {
            bool hasAny = await _context.Funcionario.AnyAsync(x => x.ID == obj.ID);
            if (!hasAny)
            {
                throw new KeyNotFoundException("ID não encontrada");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }

        public async Task InsertAsync(Funcionario obj)
        {
            if (!obj.Administrador) //Se não for administrador, cria configuração para funcionário
            {
                obj.Configuracao = new Configuracao
                {
                    Domingo = false,
                    Segunda = false,
                    Terca = false,
                    Quarta = false,
                    Quinta = false,
                    Sexta = false,
                    Sabado = false,
                    PeriodoAtendimento = 30,
                    HrInicio = new TimeSpan(7, 0, 0),
                    HrFim = new TimeSpan(18, 0, 0),
                    HrPausaInicio = new TimeSpan(12, 0, 0),
                    HrPausaFim = new TimeSpan(14, 0, 0),
                    AgendaMultipla = false
                };
            }
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Funcionario.Find(id);
                _context.Funcionario.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public (int codigo, Funcionario usuario) VerificarLogin(string login, string senha)
        {
            Funcionario conta = _context.Funcionario.Where(a => a.Login == login).FirstOrDefault();
            if (conta != null)
            {
                if(conta.Senha == senha)
                {
                    var claims = new List<Claim>
                    {
                        //new Claim(ClaimTypes.Name, login),
                        //new Claim("FuncionarioID", conta.ID.ToString())
                    };

                    if (conta.Administrador)
                    {
                        //claims.Add(new Claim("TipoUsuario", "admin"));
                        return (2, conta);
                    }
                    else
                    {
                        //claims.Add(new Claim("TipoUsuario", "funcionario"));
                        return (1, conta);
                    }
                }
                else
                {
                    return (0, null);
                }
            }
            return (-1, null);
        }
    }
}
