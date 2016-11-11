using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebVansSite.Models
{
    public class SolicitacaoModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime DataSolicitacao { get; set; }

        [Required]
        public virtual ApplicationUser Solicitante { get; set; }

        [Required]
        public string Descricao { get; set; }

        public virtual VanModel Van { get; set; }
    }
}