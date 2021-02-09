using ParqueAPICentral.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Cliente
    {

        public long ClienteID { get; set; }

        public string NomeCliente { get; set; }

        public string EmailCliente { get; set; }

        public int NifCliente { get; set; }

        public string MetodoPagamento { get; set; }

        public float Credito { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        //public Cliente(long Clienteid, string nomecliente, string emailcliente, int nifcliente, string metodopagamento, float credito)
        //{
        //    ClienteID = Clienteid;
        //    NomeCliente = nomecliente;
        //    EmailCliente = emailcliente;
        //    NifCliente = nifcliente;
        //    MetodoPagamento = metodopagamento;
        //    Credito = credito;
        //}
    }
}

