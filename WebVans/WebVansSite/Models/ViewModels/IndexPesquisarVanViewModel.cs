using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVansSite.Models.ViewModels
{
    public class IndexPesquisarVanViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "O Serviço é obrigatório.")]
        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "O Serviço é obrigatório.")]
        public int TipoServicoId { get; set; }

        [Display(Name = "Locomoção")]
        public int? TipoLocomocaoId { get; set; }

        [Display(Name = "Carga")]
        public int? TipoCargaId { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoVanId { get; set; }

        public SelectList TipoServico { get; set; }

        public SelectList Estado { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O Estado é obrigatório.")]
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public int EstadoId { get; set; }

        [Display(Name = "Cidade")]
        public int? CidadeId { get; set; }
    }
}