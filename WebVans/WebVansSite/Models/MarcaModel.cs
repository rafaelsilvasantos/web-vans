using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebVansSite.Models
{
    public class MarcaModel
    {
        public MarcaModel()
        {
            Modelos = new List<ModeloModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public virtual ICollection<ModeloModel> Modelos { get; set; }
    }
}
