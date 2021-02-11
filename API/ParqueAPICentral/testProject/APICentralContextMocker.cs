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
    class ParquePrivateAPIContextMocker
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
            dbContext.SaveChanges();
        }
    }
}
