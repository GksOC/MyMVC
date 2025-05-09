using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScndMVC.Filters;
using ScndMVC.Models;
using ScndMVC.Data;
using ScndMVC.Models.Services;
using ScndMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ScndMVC.Models.Services.Exceptions;
using System.Diagnostics;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace ScndMVC.Controllers
{
    public class AgendamentoController : Controller
    {
        //dependências
        private readonly AgendamentoService _agendamentoService;
        private readonly ServicoService _servicoService;

        public AgendamentoController (AgendamentoService agendamentoService, ServicoService servicoService)
        {
            _agendamentoService = agendamentoService;
            _servicoService = servicoService;
        }

        [Autorizacao("func")]
        public async Task<ActionResult> Index()
        {
            int funcionarioID = HttpContext.Session.GetInt32("FuncionarioID").Value;
            List<Agendamento> list = await _agendamentoService.VerificarAgendamentoHoje(funcionarioID);
            if(list.Count == 0)
            {
                list = await _agendamentoService.CriarAgendamentoDoDia(funcionarioID, DateTime.Now.Date);
            }
            list = list.OrderBy(x => x.HrAgendamento).ToList();
            return View(list);
        }

        [Autorizacao("func")]
        [HttpGet]
        public async Task<IActionResult> SelecionarAgendamento(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });

            var agendamento = await _agendamentoService.FindByIDAsync(id.Value);

            if (agendamento == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe Agendamento com ID ("+id.Value+")" });

            var servicos = await _servicoService.FindAllAsync(HttpContext.Session.GetInt32("FuncionarioID").Value);
            int? temp = null;
            if(agendamento.Servico != null)
            {
                temp = agendamento.Servico.ID;
            }

            var dto = new AgendamentoViewModel
            {
                Agendamento = agendamento,
                Servicos = servicos,
                ServicoSelecionado = temp
            };

            return Json(dto);
        }

        [Autorizacao("func")]
        [HttpPost]
        public async Task<IActionResult> Agendar([FromBody] AgendarViewModel dto)
        {
            Agendamento a = await _agendamentoService.FindByIDAsync(dto.AgendamentoID);
            if (dto.ServicoID == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Nenhum serviço foi selecionado" });
            }
            Servico s = await _servicoService.FindByFuncionarioIDAsync(HttpContext.Session.GetInt32("FuncionarioID").Value, dto.ServicoID.Value);

            a.Servico = s; //adicionando Serviço ao Agendamento
            a.Valor = dto.Valor;
            a.NmCliente = dto.NmCliente;
            a.atualizarModificao(HttpContext.Session.GetInt32("FuncionarioID").Value);
            a.Stats = Models.Enums.Status.Agendado;
            await _agendamentoService.UpdateAsync(a);

            return Ok();
        }

        [Autorizacao("func")]
        [HttpPost]
        public async Task<IActionResult> Finalizar([FromBody] AgendarViewModel dto)
        {
            Agendamento a = await _agendamentoService.FindByIDAsync(dto.AgendamentoID);
            if (dto.Valor <= 0f)
            {
                return RedirectToAction(nameof(Error), new { message = "Informe um valor válido!" });
            }

            a.Valor = dto.Valor;
            a.Stats = Models.Enums.Status.Realizado;
            a.atualizarModificao(HttpContext.Session.GetInt32("FuncionarioID").Value);
            await _agendamentoService.UpdateAsync(a);

            return Ok();
        }

        [Autorizacao("func")]
        [HttpGet]
        public async Task<IActionResult> Cancelar(int id)
        {
            Agendamento a = await _agendamentoService.FindByIDAsync(id);
            a.Valor = null;
            a.Stats = Models.Enums.Status.Cancelado;
            a.atualizarModificao(Session.FuncionarioID(HttpContext));
            await _agendamentoService.UpdateAsync(a);

            Agendamento novoA = new Agendamento
            {
                FuncionarioID = a.FuncionarioID,
                DtDia = a.DtDia,
                HrAgendamento = a.HrAgendamento,
                Stats = Models.Enums.Status.Aberto
            };
            novoA.adicionarCriador(Session.FuncionarioID(HttpContext));
            await _agendamentoService.InsertAsync(novoA, Session.FuncionarioID(HttpContext));

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de integridade! "+ e });
            }
        }

        [Autorizacao("func")]
        [HttpGet]
        public async Task<IActionResult> GerarAgendamentoExtra()
        {
            Agendamento agendamento = new Agendamento
            {
                FuncionarioID = Session.FuncionarioID(HttpContext),
                DtDia = DateTime.Now.Date
            };
            agendamento.adicionarCriador(Session.FuncionarioID(HttpContext));

            var servicos = await _servicoService.FindAllAsync(HttpContext.Session.GetInt32("FuncionarioID").Value);

            var dto = new AgendamentoViewModel
            {
                Agendamento = agendamento,
                Servicos = servicos,
                ServicoSelecionado = null
            };

            return Json(dto);
        }

        [Autorizacao("func")]
        [HttpPost]
        public async Task<IActionResult> AgendarExtra([FromBody] JsonElement dto)
        {
            //using var reader = new StreamReader(Request.Body); //para debugar o objeto JSON que vem da requisição, precisa apagar o "[FromBody]" para ler
            //var rawJson = await reader.ReadToEndAsync();

            if (dto.GetProperty("servicoSelecionado").ValueKind == JsonValueKind.Null)
            {
                return RedirectToAction(nameof(Error), new { message = "Nenhum serviço foi selecionado" });
            }
            Agendamento agendamento = new Agendamento();

            if (dto.TryGetProperty("agendamento", out JsonElement a))
            {
                agendamento.NmCliente = a.GetProperty("nmCliente").GetString();
                agendamento.Valor = (float) a.GetProperty("valor").GetSingle();
                agendamento.DtDia = DateTime.Parse(a.GetProperty("dtDia").GetString());
                agendamento.HrAgendamento = TimeSpan.Parse(a.GetProperty("hrAgendamento").GetString());
                agendamento.Stats = Models.Enums.Status.Agendado;

                agendamento.Servico = await _servicoService.FindByFuncionarioIDAsync(Session.FuncionarioID(HttpContext), dto.GetProperty("servicoSelecionado").GetInt32());
            }

            agendamento.adicionarCriador(Session.FuncionarioID(HttpContext));
            await _agendamentoService.InsertAsync(agendamento, Session.FuncionarioID(HttpContext));

            return Ok();
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}
