using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebVansSite.Models
{
    public class TipoServicoModel
    {
        public TipoServicoModel()
        {
            Vans = new List<VanModel>();            
        }

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public virtual ICollection<VanModel> Vans { get; set; }
    }
}
