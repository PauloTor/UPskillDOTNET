using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPIPrivado.Models
{
    public class Lugar
    {
        public long LugarID { get; set; }
        public int Fila { get; set; }
        public int Sector { get; set; }
        public float Preço { get; set; }
        public long ParqueID { get; set; }
        public  Parque parque { get; set; }
    }
}
