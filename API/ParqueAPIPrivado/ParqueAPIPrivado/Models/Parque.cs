﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParqueAPIPrivado.Models
{
    public enum TipoParque { publico, privado }

    public class Parque
    {
        public long ParqueID { get; set; }

        public string NomeParque { get; set; }

        public TipoParque TipoParque { get; set; }

        public int Lotacao { get; set; }
        
        [ForeignKey("MoradaID")]
        public long MoradaID { get; set; }
        public Morada Morada { get; set; }
    }
}
