using Microsoft.AspNetCore.Http;

namespace ScndMVC.Data
{
    public static class Session
    {
        public static int FuncionarioID(HttpContext context)
        {
            return context.Session.GetInt32("FuncionarioID") ?? 0;
        }

        public static string NmProfissional(HttpContext context)
        {
            return context.Session.GetString("Nome");
        }
    }
}
