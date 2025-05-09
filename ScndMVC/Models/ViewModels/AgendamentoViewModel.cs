using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScndMVC.Models.ViewModels
{
    public class AgendamentoViewModel
    {
        [JsonPropertyName("agendamento")]
        public Agendamento Agendamento { get; set; }

        [JsonPropertyName("servicos")]
        public ICollection<Servico> Servicos { get; set; }

        [JsonPropertyName("servicoSelecionado")]
        public int? ServicoSelecionado { get; set; }
    }
}
