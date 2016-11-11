using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebVansSite.Models
{
    public class EstadoModel
    {
        public EstadoModel()
        {
            Cidades = new List<CidadeModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Sigla { get; set; }

        [Required]
        public string Nome { get; set; }

        public virtual ICollection<CidadeModel> Cidades { get; set; }
    }
}
