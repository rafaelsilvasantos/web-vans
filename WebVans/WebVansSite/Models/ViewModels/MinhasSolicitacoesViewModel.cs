using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebVansSite.Models.ViewModels
{
    public class MinhasSolicitacoesViewModel
    {
        public IEnumerable<SolicitacaoViewModel> SolicitacoesParaMim { get; set; }

        public IEnumerable<SolicitacaoViewModel> SolicitacoesFeitasPorMim { get; set; }   
    }

    public class SolicitacaoViewModel
    {
        public int ID { get; set; }

        public DateTime DataSolicitacao { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string CidadeEstado { get; set; }

        public string TipoServico { get; set; }

        public string NomeSolicitante { get; set; }

        public string TelefoneSolicitante { get; set; }

        public string EmailSolicitante { get; set; }

        public string NomeProprietario { get; set; }

        public string TelefoneProprietario { get; set; }

        public string EmailProprietario { get; set; }
    }
}