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


namespace ScndMVC.Controllers
{
    public class AgendamentoController : Controller
    {
        //dependências
        private readonly AgendamentoService _agendamentoService;

        public AgendamentoController (AgendamentoService AgendamentoService)
        {
            _agendamentoService = AgendamentoService;
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
        public IActionResult Create()
        {
            return View();
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID, NmProfissional, Telefone, Email, Login, Senha, Administrador, Configuracao")]
            Funcionario funcionario)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                return View();
            }

            try
            {
                funcionario.adicionarCriador(HttpContext.Session.GetInt32("FuncionarioID").Value);
                await _agendamentoService.InsertAsync(funcionario);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Autorizacao("func")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado!" });

            var obj = await _agendamentoService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe conta com ID ("+obj.ID+")" });

            return View(obj);
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Funcionario obj)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                return View(obj);
            }

            if (id != obj.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });
            }

            try
            {
                obj.atualizarModificao(HttpContext.Session.GetInt32("FuncionarioID").Value);
                await _agendamentoService.UpdateAsync(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Autorizacao("func")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });

            var obj = await _agendamentoService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe conta com ID ("+id+")" });

            return View(obj);
        }

        [Autorizacao("func")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });

            var obj = await _agendamentoService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe conta com ID (" + id + ")" });

            return View(obj);
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _agendamentoService.RemoveAsync(id);
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
