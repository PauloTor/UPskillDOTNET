﻿using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPI.Models
{
    public class SubAluguer
    {
        public long SubAluguerID { get; set; }
        public long ReservaID { get; set; }
        public int ClienteID { get; set; }
        public float Preço { get; set; }
        public Date DataInicio { get; set; }
        public Date DataFim { get; set; }
    }
}
