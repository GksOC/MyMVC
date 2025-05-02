using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public int VerificarLogin(string login, string senha)
        {
            Funcionario conta = _context.Funcionario.Where(a => a.Login == login).FirstOrDefault();
            if (conta != null)
            {
                if(conta.Senha == senha)
                {
                    if (conta.Administrador)
                    {
                        return 2;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    return 0;
                }
            }
            return -1;
        }
    }
}
