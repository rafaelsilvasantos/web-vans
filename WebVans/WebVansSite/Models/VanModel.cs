using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebVansSite.Models
{
    public class VanModel
    {
        public VanModel()
        {
            CidadesAtendidas = new List<CidadeModel>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public virtual ModeloModel Modelo { get; set; }

        [Required]
        public int Ano { get; set; }

        [Required]
        public int TipoServicoId { get; set; }

        [Required]
        public virtual TipoServicoModel TipoServico { get; set; }

        public virtual TipoVanModel TipoVan { get; set; }

        public virtual TipoCargaModel TipoCarga { get; set; }

        public virtual TipoLocomocaoModel TipoLocomocao { get; set; }

        public virtual ICollection<CidadeModel> CidadesAtendidas { get; set; }

        public string Observacoes { get; set; }

        [Required]
        public DateTime DataCadastro { get; set; }

        [Required]
        public virtual ApplicationUser Proprietario { get; set; }

        public virtual ICollection<SolicitacaoModel> Solicitacoes { get; set; }

        [Required]
        public string Placa { get; set; }
    }
}
