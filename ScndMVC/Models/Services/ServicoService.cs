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
    public class ServicoService
    {
        private readonly MainContext _context;

        public ServicoService(MainContext context)
        {
            _context = context;
        }

        public async Task<List<Servico>> FindAllAsync(int id)
        {
            return await _context.Servico.Where(x => x.FuncionarioID == id).ToListAsync();
        }

        public async Task<Servico> FindByIDAsync(int id)
        {
            return await _context.Servico.FirstOrDefaultAsync(obj => obj.ID == id);
        }

        public async Task<Servico> FindByFuncionarioIDAsync(int funcionarioID, int servicoID)
        {
            return await _context.Servico.FirstOrDefaultAsync(obj => obj.FuncionarioID == funcionarioID && obj.ID == servicoID);
        }

        public async Task UpdateAsync(Servico obj)
        {
            bool hasAny = await _context.Servico.AnyAsync(x => x.ID == obj.ID);
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

        public async Task InsertAsync(Servico obj, int id)
        {
            var func = await _context.Funcionario.FirstOrDefaultAsync(x => x.ID == id);
            func.Servicos.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = _context.Servico.Find(id);
                _context.Servico.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
    }
}
