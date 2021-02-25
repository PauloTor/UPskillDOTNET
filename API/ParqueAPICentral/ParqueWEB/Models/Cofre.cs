using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParqueAPICentral.Models
{
    public class Cofre
    {
        public static float Saldo { get; set; }


        public static void Entrada(float valor)
        {
            Saldo += valor;
        }

        public static void Saida(float valor)
        {
            Saldo -= valor;
        }
    }
}
