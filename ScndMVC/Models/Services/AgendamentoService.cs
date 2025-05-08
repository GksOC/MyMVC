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
    public class AgendamentoService
    {
        private readonly MainContext _context;

        public AgendamentoService(MainContext context)
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

        public async Task<List<Agendamento>> VerificarAgendamentoHoje(int id)
        {
            return await _context.Agendamento.Where(x => x.FuncionarioID == id && x.DtDia == DateTime.Now.Date).ToListAsync();
        }

        public async Task<List<Agendamento>> CriarAgendamentoDoDia(int id, DateTime data)
        {
            Funcionario func = await _context.Funcionario.Include(x => x.Configuracao).Where(y => y.ID == id).FirstAsync(); //obtendo funcionário
            Configuracao config = func.Configuracao; //obtendo configuração do funcionário
            int totalTempo = (int) ( ( (config.HrFim - config.HrInicio) - (config.HrPausaFim - config.HrPausaInicio) ).TotalMinutes ); //obtendo tempo total do expediente

            List<Agendamento> list = new List<Agendamento>();
            TimeSpan hora = config.HrInicio;
            while (totalTempo > 0)
            {
                if(hora < config.HrPausaInicio || hora >= config.HrPausaFim) //verifica se está dentro do expediente
                {
                    Agendamento agendamento = new Agendamento
                    {
                        FuncionarioID = id, //tunelamento
                        DtDia = data, //data passada pelo parâmetro
                        HrAgendamento = hora,
                        Stats = Enums.Status.Aberto
                    };
                    agendamento.adicionarCriador(id);
                    func.Agendamentos.Add(agendamento);
                    list.Add(agendamento);
                    totalTempo -= config.PeriodoAtendimento;
                }
                hora += TimeSpan.FromMinutes(config.PeriodoAtendimento);
            }
            await _context.SaveChangesAsync();

            return list;
        }
    }
}
