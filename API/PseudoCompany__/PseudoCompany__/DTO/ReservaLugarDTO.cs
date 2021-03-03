using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.DTO
{
    public class ReservaLugarDTO

    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public float PrecoLugar { get; set; }
    }
}
