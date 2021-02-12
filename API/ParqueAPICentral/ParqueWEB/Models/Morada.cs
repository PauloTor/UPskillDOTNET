using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Morada
    {
        public long MoradaID { get; set; }

        public string Rua { get; set; }

        public string CodigoPostal { get; set; }

        public Morada(string rua, string postal)
        {
            Rua = rua;
            CodigoPostal = postal;
        }
    }
}
