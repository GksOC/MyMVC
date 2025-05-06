using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScndMVC.Models;
using ScndMVC.Models.Services;
using ScndMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        [Authorize]
        public ActionResult Login()
        {
            Console.WriteLine("teste");
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel lvm, string returnUrl = null)
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
                    await autentica(tupla.claims);
                    ViewBag.TipoUser = "funcionario";
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl); //página desejada
                    }
                    return RedirectToAction("Index", "Departments"); //página padrão
                    //break;
                case 2:
                    await autentica(tupla.claims);
                    ViewBag.TipoUser = "admin";
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl); //página desejada
                    }
                    return RedirectToAction("Index", "Sellers"); //página padrão
                    //break;
                default:
                    return View();
            }
            //return View();
        }

        public async Task autentica(List<Claim> claims)
        {
            var identidade = new ClaimsIdentity(claims, "CookieAuth");
            var usuarioPrincipal = new ClaimsPrincipal(identidade);

            await HttpContext.SignInAsync("CookieAuth", usuarioPrincipal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login", "Funcionario");
        }
    }
}
