using Microsoft.AspNetCore.Mvc;
using ScndMVC.Models;
using ScndMVC.Models.Services;
using ScndMVC.Models.ViewModels;
using System;
using System.Threading.Tasks;


namespace ScndMVC.Controllers
{
    public class FuncionarioController : Controller
    {
        //dependências
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController (FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChecarLogin(LoginViewModel lvm)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                var funcionario = await _funcionarioService.FindAllAsync();
                return View(funcionario);
            }

            switch( _funcionarioService.VerificarLogin(lvm.Login, lvm.Senha))
            {
                case -1:
                    ViewBag.Erro = "Usuário não encontrado.";
                    return View("Index");
                case 0:
                    ViewBag.Erro = "Senha incorreta.";
                    return View("Index");
                case 1:
                    return View("Index");
                case 2:
                    return View("Index");
                default:
                    return View("Index");
            }
        }
    }
}
