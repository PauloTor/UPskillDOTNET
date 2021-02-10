using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Parque
    {
        public long ParqueID { get; set; }

        public string NomeParque { get; set; }

        public Parque(string nomeParque)
        {
            NomeParque = nomeParque;
        }


    }
}
