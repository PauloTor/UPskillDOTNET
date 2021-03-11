using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace ParqueAPICentral.DTO
{
    public class ParquePrivadoReservaDTO
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public long ReservaID { get; set; }
        public long LugarID { get; set; }
    }
}
