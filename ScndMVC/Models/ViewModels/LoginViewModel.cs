using System;
using System.ComponentModel.DataAnnotations;

namespace ScndMVC.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} requerido!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "{0} requerida!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string UserID { get; set; }
        public string TipoUser { get; set; }
    }
}
