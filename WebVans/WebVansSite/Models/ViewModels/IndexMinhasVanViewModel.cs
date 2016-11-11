﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVansSite.Models.ViewModels
{
    public class IndexMinhasVanViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Display(Name = "Serviço")]
        public string TipoServico { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Cidade")]
        public string CidadeEstado { get; set; }

        [Display(Name = "Placa")]
        public string Placa { get; set; }
    }
}