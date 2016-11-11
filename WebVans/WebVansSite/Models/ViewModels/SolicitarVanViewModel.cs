﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVansSite.Models.ViewModels
{
    public class SolicitarVanViewModel
    {
        public SolicitarVanViewModel()
        {
            SolicitacaoConcluida = false;
        }

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
        public string Proprietario { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage="Descreva com mais detalhes a sua solicitação.")]
        [StringLength(int.MaxValue, MinimumLength=20, ErrorMessage="Descreva com mais detalhes sua solicitação.")]
        public string Descricao { get; set; }

        public bool SolicitacaoConcluida { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeEstado { get; set; }
        
    }
}