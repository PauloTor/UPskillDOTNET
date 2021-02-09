using ParqueAPICentral.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Cliente : User
    {
       
        public long ClienteID { get; set; }

        public string NomeCliente { get; set; }

        public string EmailCliente { get; set; }

        public int NifCliente { get; set; }

        public string MetodoPagamento { get; set; }

        public decimal Credito { get; set; }

        public virtual void Deposit(decimal valor)
        {
            Credito += valor;
        }

        /*private readonly List<Fatura> Operacao = new List<Fatura>();

        public decimal Creditos
        {
            get
            {
                decimal Creditos = 0;
                foreach (var item in Operacao)
                {
                    Creditos -= item.PrecoFatura;
                }

                return Credito;
            }
        }
        public decimal DevolverPagamento(decimal credito, decimal precoFatura)
        {
            this.Credito = credito;
            
            return Credito += precoFatura;
        }
        */
    }
}
