using System.Collections.Generic;

namespace ScndMVC.Models.ViewModels
{
    public class AgendamentoViewModel
    {
        public Agendamento Agendamento { get; set; }
        public ICollection<Servico> Servicos { get; set; }
        public int? ServicoSelecionado { get; set; }
    }
}
