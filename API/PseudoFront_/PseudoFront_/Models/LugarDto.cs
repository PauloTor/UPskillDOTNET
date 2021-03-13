using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PseudoFront_.Models;

namespace PseudoFront_.Models
{
    public class LugarDTO
    {
        public long LugarID { get; set; }
        public int Fila { get; set; }
        public int Sector { get; set; }
        public float Preço { get; set; }

        
    }
}
