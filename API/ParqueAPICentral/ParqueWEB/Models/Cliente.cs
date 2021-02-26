using ParqueAPICentral.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ParqueAPICentral.Models;

namespace ParqueAPICentral.Models
{
    public class Cliente
    {
        [Key]
        public long ClienteID { get; set; }

        public string NomeCliente { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailCliente { get; set; }

        [Required]
        [RegularExpression(@"^\\d{9}$", ErrorMessage = "NIF length must be 9 numbers")]
        public int NifCliente { get; set; }

        public string MetodoPagamento { get; set; }

        public float Credito { get; set; }

        [ForeignKey("Id")]
        public long Id { get; set; }
        public User User { get; set; }


        public Cliente(string nomeCliente, string emailCliente, int nifCliente, string metodoPagamento, float credito,long id)
        {
            NomeCliente = nomeCliente;
            EmailCliente = emailCliente;
            NifCliente = nifCliente;
            MetodoPagamento = metodoPagamento;
            Credito = credito;
            Id = id;
        }


        public void Depositar(float valor)
        {
            if (valor < 0)
            {
                throw new Exception("Não é possível depositar uma quantia negativa.");
            }
            else
            {
                Cofre.Saida(valor);
                Credito += valor;
            }
        }

        public void Pagar(float valor)
        {
            if (valor < 0)
            {
                throw new Exception("A quantia a pagar deve ter um valor positivo.");
            }
            else
            {                
                Credito -= valor;
                Cofre.Entrada(valor);
            }
            if (Credito < 0)
            {
                Cofre.Saida(valor);
                Credito += valor;
                throw new Exception("O crédito não permite efetuar a operação.");
            }
        }
    }   
}

