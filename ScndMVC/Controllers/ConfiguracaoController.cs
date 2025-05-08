using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScndMVC.Filters;
using ScndMVC.Models;
using ScndMVC.Models.Services;
using ScndMVC.Models.ViewModels;
using System;
using System.Threading.Tasks;
using System.Diagnostics;


namespace ScndMVC.Controllers
{
    public class ConfiguracaoController : Controller
    {
        //dependências
        private readonly ConfiguracaoService _configuracaoService;

        public ConfiguracaoController (ConfiguracaoService configuracaoService)
        {
            _configuracaoService = configuracaoService;
        }

        [Autorizacao("func")]
        public async Task<IActionResult> Index()
        {
            Configuracao obj = await _configuracaoService.FindByIDAsync(HttpContext.Session.GetInt32("FuncionarioID").Value);
            return View(obj);
        }

        [Autorizacao("func")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Configuracao obj)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                TempData["Erro"] = "Um erro foi identificado!";
                return View("Index", obj);
            }

            if (id != obj.ID)
            {
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });
            }

            try
            {
                obj.atualizarModificao(HttpContext.Session.GetInt32("FuncionarioID").Value);
                await _configuracaoService.UpdateAsync(obj);
                ViewBag.Mensagem = "Alterações salvas";
                return View("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
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
