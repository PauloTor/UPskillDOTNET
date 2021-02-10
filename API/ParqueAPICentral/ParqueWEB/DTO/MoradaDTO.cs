using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.DTO
{
    public class Morada
    {
        public long MoradaID { get; set; }

        public string Rua { get; set; }

        public string CodigoPostal { get; set; }
    }
}
