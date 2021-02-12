using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace ParqueAPICentral.Models

{
    public class Parque
    {
        [Key]
        public long ParqueID { get; set; }

        public string NomeParque { get; set; }

        public int NIFParque { get; set; }

        public int Lotacao { get; set; }
        [ForeignKey("MoradaID")]
        public long MoradaID { get; set; }

        public string Url { get; set; }
        public Morada morada {get;set;}
        
        public Parque(string nomeParque)
        {
        }


    }
}
 