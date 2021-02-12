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

        public long NIFParque { get; set; }

        public int Lotacao { get; set; }      
        
        [Url]
        public string Url { get; set; }

        [ForeignKey("MoradaID")]
        public long MoradaID { get; set; }
        public Morada Morada {get;set;}
        
        public Parque(string nomeParque, long nifParque, int lotacao, string url, long moradaId)
        {
            NomeParque = nomeParque;
            NIFParque = nifParque;
            Lotacao = lotacao;
            Url = url;
            MoradaID = moradaId;
        }
    }
}
 