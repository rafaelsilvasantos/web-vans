using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebVansSite.Models.ViewModels
{
    public class EditarVanViewModel
    {
        public EditarVanViewModel()
        {
            TipoLocomocao = Enumerable.Empty<SelectListItem>();
            TipoCarga = Enumerable.Empty<SelectListItem>();
            TipoVan = Enumerable.Empty<SelectListItem>();
        }

        public int ID { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A Marca é obrigatória.")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "A Marca é obrigatória.")]
        public int MarcaId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "O Modelo é obrigatório.")]
        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "O Modelo é obrigatório.")]
        public int ModeloId { get; set; }

        [Range(0, 3000, ErrorMessage = "O Ano está inválido")]
        [Display(Name = "Ano")]
        [Required(ErrorMessage = "O Ano é obrigatório.")]
        public int Ano { get; set; }

        [Range(1, int.MaxValue, ErrorMessage="O Serviço é obrigatório.")]
        [Display(Name = "Serviço")]
        [Required(ErrorMessage = "O Serviço é obrigatório.")]
        public int TipoServicoId { get; set; }

        [Display(Name = "Locomoção")]
        public int? TipoLocomocaoId { get; set; }

        public IEnumerable<SelectListItem> TipoLocomocao { get; set; }

        [Display(Name = "Carga")]
        public int? TipoCargaId { get; set; }

        public IEnumerable<SelectListItem> TipoCarga { get; set; }

        [Display(Name = "Tipo")]
        public int? TipoVanId { get; set; }

        public IEnumerable<SelectListItem> TipoVan { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        public SelectList ListaTipoServico { get; set; }
        
        public SelectList ListaEstado { get; set; }

        public SelectList ListaCidade { get; set; }

        public SelectList ListaMarca { get; set; }

        public SelectList ListaModelo { get; set; }
        
        [Range(1, int.MaxValue, ErrorMessage = "O Estado é obrigatório.")]
        [Display(Name = "Estado")]
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public int EstadoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "A Cidade é obrigatória.")]
        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "A Cidade é obrigatória.")]
        public int CidadeId { get; set; }

        [Required(ErrorMessage = "A Placa é obrigatória.")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }
    }
}