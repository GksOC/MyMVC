using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScndMVC.Filters;
using ScndMVC.Models;
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
                return View(list);
            }
            return View(list);
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit([FromBody] Agendamento obj)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                return Json(new { success = false, message = "Erro no model!" }); ;
            }

            try
            {
                await _agendamentoService.UpdateAsync(obj);
                return Json(new { success = true, message = "Cadastrado com sucesso!" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Erro ao cadastrar o agendamento!" });
            }
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
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });

            var obj = await _agendamentoService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe agendamento com ID (" + id + ")" });

            return View(obj);
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancelar(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Erro de integridade!" });
            }
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
