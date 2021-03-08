using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PseudoFront_.Models
{
    public class ParqueDTO
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
        public MoradaDTO Moradadto {get;set;}
        
        public ParqueDTO()
        {
        }

        public ParqueDTO(string nomeParque, long nifParque, int lotacao, string url, long moradaId)
        {
            NomeParque = nomeParque;
            NIFParque = nifParque;
            Lotacao = lotacao;
            Url = url;
            MoradaID = moradaId;
        }
    }
}
 