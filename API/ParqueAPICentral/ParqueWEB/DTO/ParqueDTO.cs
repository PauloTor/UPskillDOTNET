using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.DTO
{
    public class Parque
    {
        [Key]
        public long ParqueID { get; set; }

        public long NifParque { get; set; }

        public string NomeParque { get; set; }

        public int Lotacao { get; set; }

        [ForeignKey("MoradaID")]
        public long MoradaID { get; set; }
        public Morada Morada { get; set; }
    }
}
