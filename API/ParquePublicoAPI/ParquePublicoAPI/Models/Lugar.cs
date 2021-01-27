using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParquePublicoAPI.Models
{
    public class Lugar
    {
        public long LugarID { get; set; }

        public float PrecoLugar { get; set; }

        [ForeignKey("RuaID")]
        public long RuaID { get; set; }
        public Rua Rua { get; set; }
    }
}
