using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVansSite.Models.ViewModels
{
    public class DetalheSolicitacaoViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Display(Name = "Serviço")]
        public string TipoServico { get; set; }

        [Display(Name = "Tipo")]
        public string TipoVan { get; set; }

        [Display(Name = "Carga")]
        public string TipoCarga { get; set; }

        [Display(Name = "Locomoção")]
        public string TipoLocomocao { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Display(Name = "Cadastrada em")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Proprietário")]
        public string NomeProprietario { get; set; }

        [Display(Name = "Telefone")]
        public string TelefoneProprietario { get; set; }

        [Display(Name = "E=mail")]
        public string EmailProprietario { get; set; }

        [Display(Name = "Solicitante")]
        public string NomeSolicitante { get; set; }

        [Display(Name = "Telefone")]
        public string TelefoneSolicitante { get; set; }

        [Display(Name = "E-mail")]
        public string EmailSolicitante { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Solicitada em")]
        public DateTime DataSolicitacao { get; set; }

        public bool MinhaVan { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeEstado { get; set; }
        
    }
}