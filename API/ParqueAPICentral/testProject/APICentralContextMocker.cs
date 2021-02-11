using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParqueAPICentral.Data;
using ParqueAPICentral.Models;

namespace testProject
{
    class APICentralContextMocker
    {
        private static APICentralContext dbContext;
        public static APICentralContext GetAPICentralContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<APICentralContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;
            dbContext = new APICentralContext(options);
            Seed();
            return dbContext;
        }

        public static void Seed()
        {
            dbContext.Cliente.Add(new Cliente { ClienteID = 1, NomeCliente = "Victor Duarte", EmailCliente = "upskill1@upskill.pt", NifCliente = 123456789, MetodoPagamento = "PayPal", Credito = 15, Id = 1 });
            dbContext.Cliente.Add(new Cliente { ClienteID = 2, NomeCliente = "Pedro Casimiro", EmailCliente = "upskill2@upskill.pt", NifCliente = 112345678, MetodoPagamento = "MB", Credito = 5, Id = 2 });
            dbContext.Cliente.Add(new Cliente { ClienteID = 3, NomeCliente = "Leandro Caetano", EmailCliente = "upskill3@upskill.pt", NifCliente = 122345678, MetodoPagamento = "PayPal", Credito = 0, Id = 3 });

            dbContext.SaveChanges();
        }
    }
}
