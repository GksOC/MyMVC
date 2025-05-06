using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public (int codigo, List<Claim> claims) VerificarLogin(string login, string senha)
        {
            Funcionario conta = _context.Funcionario.Where(a => a.Login == login).FirstOrDefault();
            if (conta != null)
            {
                if(conta.Senha == senha)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, login),
                        new Claim("FuncionarioID", conta.ID.ToString())
                    };

                    if (conta.Administrador)
                    {
                        claims.Add(new Claim("TipoUsuario", "admin"));
                        return (2, claims);
                    }
                    else
                    {
                        claims.Add(new Claim("TipoUsuario", "funcionario"));
                        return (1, claims);
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
