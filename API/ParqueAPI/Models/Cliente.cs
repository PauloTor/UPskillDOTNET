﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPI.Models
{
    public class Cliente
    {
        public long ClienteID { get; set; }
        public string NomeCliente { get; set; }
        public string EmailCliente { get; set; }
        public long NifCliente { get; set; }
        public string MetodoPagamento { get; set; }
        public float Credito { get; set; }

    }
}
