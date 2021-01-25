using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueAPI.Models
{
    public class Morada
    {
        public long MoradaID { get; set; }

        public string Rua { get; set; }

        public string CodigoPostal { get; set; }
    }
}
