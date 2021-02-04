using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ParqueAPICentral.Models;

namespace ParquePrivateAPI.Models
{
    public class Lugar
    {
        public long LugarID { get; set; }
        public int Fila { get; set; }
        public int Sector { get; set; }
        public float Preço { get; set; }

        [ForeignKey("ParqueID")]
        public long ParqueID { get; set; }
        public  Parque Parque { get; set; }
    }
}
