using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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

        public ActionResult Login()
        {
            Console.WriteLine("teste");
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
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
                    ViewBag.TipoUser = "funcionario";
                    //if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl); //página desejada
                    //}
                    return RedirectToAction("Index", "Departments"); //página padrão
                    //break;
                case 2:
                    //await autentica(tupla.claims);
                    HttpContext.Session.SetInt32("FuncionarioID", tupla.usuario.ID);
                    HttpContext.Session.SetString("Nome", tupla.usuario.NmProfissional);
                    HttpContext.Session.SetString("Tipo", "admin");
                    ViewBag.TipoUser = "admin";
                    //if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    //{
                    //    return Redirect(returnUrl); //página desejada
                    //}
                    return RedirectToAction("Index", "Sellers"); //página padrão
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
            return RedirectToAction("Index", "Funcionario");
        }
    }
}
