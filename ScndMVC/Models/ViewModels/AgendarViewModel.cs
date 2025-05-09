using System.Collections.Generic;

namespace ScndMVC.Models.ViewModels
{
    public class AgendarViewModel
    {
        public int AgendamentoID {  get; set; }
        public string NmCliente { get; set; }
        public float Valor { get; set; }
        public int? ServicoID { get; set; }
    }
}
