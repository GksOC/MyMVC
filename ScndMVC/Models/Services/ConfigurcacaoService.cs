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
    public class ConfiguracaoService
    {
        private readonly MainContext _context;

        public ConfiguracaoService(MainContext context)
        {
            _context = context;
        }

        public async Task<Configuracao> FindByIDAsync(int id)
        {
            return await _context.Funcionario.Where(x => x.ID == id).Select(y => y.Configuracao).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Configuracao obj)
        {
            bool hasAny = await _context.Configuracao.AnyAsync(x => x.ID == obj.ID);
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
    }
}
