using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParquePrivateAPI.Models
{
    public class Lugar
    {
        public long LugarID { get; set; }
        public int Fila { get; set; }
        public int Sector { get; set; }
        public float Preço { get; set; }

        [ForeignKey("NIFParqueID")]
        public long NIFParqueID { get; set; }
        public  Parque Parque { get; set; }
    }
}
