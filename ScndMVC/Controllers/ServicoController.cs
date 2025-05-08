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
    public class ServicoController : Controller
    {
        //dependências
        private readonly ServicoService _servicoService;

        public ServicoController (ServicoService funcionarioService)
        {
            _servicoService = funcionarioService;
        }

        [Autorizacao("func")]
        public async Task<IActionResult> Index()
        {
            List<Servico> list = await _servicoService.FindAllAsync(HttpContext.Session.GetInt32("FuncionarioID").Value);
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
            [Bind("ID, NmServico, DsServico, Valor")]
            Servico servico)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                return View();
            }

            try
            {
                servico.adicionarCriador(HttpContext.Session.GetInt32("FuncionarioID").Value);
                servico.FuncionarioID = HttpContext.Session.GetInt32("FuncionarioID").Value;
                await _servicoService.InsertAsync(servico);
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

            var obj = await _servicoService.FindByIDAsync(id.Value);
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
                await _servicoService.UpdateAsync(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servicoService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Can't delete seller because it has sales!" });
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
