using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebVansSite.Models
{
    public class CidadeModel
    {
        public CidadeModel()
        {
            Vans = new List<VanModel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public virtual EstadoModel Estado { get; set; }

        public virtual ICollection<VanModel> Vans { get; set; }
    }
}
