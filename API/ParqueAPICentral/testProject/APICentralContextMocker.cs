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


            dbContext.SaveChanges();
        }
    }
}
