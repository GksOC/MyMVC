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
    public class FuncionarioController : Controller
    {
        //dependências
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController (FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index(string acesso = null)
        {
            ViewBag.Erro = acesso;
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm)
        {
            if (!ModelState.IsValid) //validando caso o usuário esteja com javaScript desabilitado
            {
                var funcionario = await _funcionarioService.FindAllAsync();
                return View(funcionario);
            }

            var tupla = _funcionarioService.VerificarLogin(lvm.Login, lvm.Senha);
            switch ( tupla.codigo)
            {
                case -1:
                    ViewBag.Erro = "Usuário não encontrado.";
                    return View("Index");
                case 0:
                    ViewBag.Erro = "Senha incorreta.";
                    return View("Index");
                case 1:
                    //await autentica(tupla.claims);
                    HttpContext.Session.SetInt32("FuncionarioID", tupla.usuario.ID);
                    HttpContext.Session.SetString("Nome", tupla.usuario.NmProfissional);
                    HttpContext.Session.SetString("Tipo", "func");
                    //if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl); //página desejada
                    //}
                    return RedirectToAction("Index", "Home"); //página padrão
                    //break;
                case 2:
                    //await autentica(tupla.claims);
                    HttpContext.Session.SetInt32("FuncionarioID", tupla.usuario.ID);
                    HttpContext.Session.SetString("Nome", tupla.usuario.NmProfissional);
                    HttpContext.Session.SetString("Tipo", "admin");
                    //if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl); //página desejada
                    //}
                    return RedirectToAction("Gerenciar", "Funcionario"); //página padrão
                    //break;
                default:
                    return View();
            }
            //return View();
        }

        //public async Task autentica(List<Claim> claims)
        //{
        //    var identidade = new ClaimsIdentity(claims, "CookieAuth");
        //    var usuarioPrincipal = new ClaimsPrincipal(identidade);

        //    await HttpContext.SignInAsync("CookieAuth", usuarioPrincipal);
        //}

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //await HttpContext.SignOutAsync("CookieAuth");
            return View("Index");
        }


        [Autorizacao("admin")]
        public async Task<IActionResult> Gerenciar()
        {
            var funcionarios = await _funcionarioService.FindAllAsync();
            return View(funcionarios);
        }

        [Autorizacao("admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Autorizacao("admin")]
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
                await _funcionarioService.InsertAsync(funcionario);
                return RedirectToAction(nameof(Gerenciar));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Autorizacao("admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID não encontrado!" });

            var obj = await _funcionarioService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe conta com ID ("+obj.ID+")" });

            return View(obj);
        }

        [Autorizacao("admin")]
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
                await _funcionarioService.UpdateAsync(obj);
                return RedirectToAction(nameof(Gerenciar));
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        [Autorizacao("admin")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });

            var obj = await _funcionarioService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe conta com ID ("+id+")" });

            return View(obj);
        }

        [Autorizacao("admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return RedirectToAction(nameof(Error), new { message = "ID inválida!" });

            var obj = await _funcionarioService.FindByIDAsync(id.Value);
            if (obj == null)
                return RedirectToAction(nameof(Error), new { message = "Não existe conta com ID (" + id + ")" });

            return View(obj);
        }

        [Autorizacao("admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _funcionarioService.RemoveAsync(id);
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
