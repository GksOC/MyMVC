using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ScndMVC.Filters
{
    public class AutorizacaoAttribute : ActionFilterAttribute
    {
        private readonly string _tipoRequerido;

        public AutorizacaoAttribute(string tipoRequerido = null)
        {
            _tipoRequerido = tipoRequerido;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var tipoUsuario = context.HttpContext.Session.GetString("Tipo");

            if (string.IsNullOrEmpty(tipoUsuario) || (_tipoRequerido != null && tipoUsuario != _tipoRequerido))
            {
                context.Result = new RedirectToActionResult("Index", "Funcionario", new { acesso = "Acesso não autorizado" });
            }
        }
    }
}
