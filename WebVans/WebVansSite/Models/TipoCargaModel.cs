using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebVansSite.Models
{
    public class TipoCargaModel
    {
        public TipoCargaModel()
        {
            Vans = new List<VanModel>();
            TipoVans = new List<TipoVanModel>();
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public bool Ativo { get; set; }
        
        public virtual ICollection<VanModel> Vans { get; set; }

        public virtual ICollection<TipoVanModel> TipoVans { get; set; }
    }
}
