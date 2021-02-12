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
            dbContext.Cliente.Add(new Cliente ("Victor Duarte", "upskill1@upskill.pt", 123456789, "PayPal", 15, 1));
            dbContext.Cliente.Add(new Cliente ("Pedro Casimiro", "upskill2@upskill.pt", 112345678, "MB", 5, 2));
            dbContext.Cliente.Add(new Cliente ("Leandro Caetano", "upskill3@upskill.pt", 122345678, "PayPal", 0, 3));
            //dbContext.SubAluguer.Add(new SubAluguer(15, DateTime.Parse("2020-01-01 15:00:00"), DateTime.Parse("2020-01-05 15:00:00"), DateTime.Parse("2020-01-05 16:00:00"), 1));
            //dbContext.SubAluguer.Add(new SubAluguer(15, DateTime.Parse("2020-02-01 15:00:00"), DateTime.Parse("2020-02-05 15:00:00"), DateTime.Parse("2020-02-05 16:00:00"), 2));
            //dbContext.SubAluguer.Add(new SubAluguer(15, DateTime.Parse("2020-03-01 15:00:00"), DateTime.Parse("2020-03-05 15:00:00"), DateTime.Parse("2020-03-05 16:00:00"), 3));

            dbContext.Fatura.Add(new Fatura(DateTime.Parse("2020-01-04 21:00:00"), 33, 1));
            dbContext.Fatura.Add(new Fatura(DateTime.Parse("2021-01-01 20:00:00"), 13, 2));
            dbContext.Fatura.Add(new Fatura(DateTime.Parse("2019-02-11 21:00:00"), 2, 3));

            dbContext.SaveChanges();
        }
    }
}
