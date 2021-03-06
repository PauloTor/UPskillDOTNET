//using ParqueAPICentral.Entities;
using ParqueAPICentral.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Cliente
    {
        [Key]
        public long ClienteID { get; set; }

        public string NomeCliente { get; set; }
        
        public string EmailCliente { get; set; }

        public int NifCliente { get; set; }

        public string MetodoPagamento { get; set; }

        public float Credito { get; set; }

        //[ForeignKey("Id")]
        //public long Id { get; set; }
        //public User_ User_ { get; set; }


        public Cliente(string nomeCliente, string emailCliente, int nifCliente, string metodoPagamento, float credito,long id)
        {
            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
            NifCliente = nifCliente;
            MetodoPagamento = metodoPagamento;
            Credito = credito;
           // Id = id;
        }


    }   
}

