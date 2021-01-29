using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParquePublicoAPI.Data;
using ParquePublicoAPI.Models;

namespace testProject
{
    class ParquePublicoAPIContextMocker
    {
        private static ParquePublicoAPIContext dbContext;
        public static ParquePublicoAPIContext GetParquePublicoAPIContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ParquePublicoAPIContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;
            dbContext = new ParquePublicoAPIContext(options);
            Seed();
            return dbContext;
        }

        public static void Seed()
        {
            dbContext.Rua.Add(new Rua { RuaID = 1, NomeRua = "Avenida dos Aliados", CodigoPostal = "4050-150", Lotacao = 5});
            dbContext.Rua.Add(new Rua { RuaID = 2, NomeRua = "Boavista", CodigoPostal = "4050-150", Lotacao = 5 });
            dbContext.Lugar.Add(new Lugar { LugarID = 1, PrecoLugar = 5, RuaID = 1 });
            dbContext.Lugar.Add(new Lugar { LugarID = 2, PrecoLugar = 5, RuaID = 1 });
            dbContext.Lugar.Add(new Lugar { LugarID = 3, PrecoLugar = 2, RuaID = 2 });
            dbContext.Lugar.Add(new Lugar { LugarID = 4, PrecoLugar = 3, RuaID = 2 });
            dbContext.Lugar.Add(new Lugar { LugarID = 5, PrecoLugar = 3, RuaID = 2 });
            dbContext.Reserva.Add(new Reserva { ReservaID = 1, DataReserva = DateTime.Parse("2020-01-01 15:00:00"), DataInicio = DateTime.Parse("2020-01-05 15:00:00"), DataFim = DateTime.Parse("2020-01-05 16:00:00"), LugarID = 1 });
            dbContext.Reserva.Add(new Reserva { ReservaID = 2, DataReserva = DateTime.Parse("2020-01-2 15:00:00"), DataInicio = DateTime.Parse("2020-01-04 11:00:00"), DataFim = DateTime.Parse("2020-01-04 13:00:00"), LugarID = 2 });
            dbContext.Reserva.Add(new Reserva { ReservaID = 3, DataReserva = DateTime.Parse("2019-12-15 15:00:00"), DataInicio = DateTime.Parse("2020-02-09 17:00:00"), DataFim = DateTime.Parse("2020-02-09 18:00:00"), LugarID = 3 });
            dbContext.Reserva.Add(new Reserva { ReservaID = 4, DataReserva = DateTime.Parse("2019-12-10 10:00:00"), DataInicio = DateTime.Parse("2020-02-01 11:00:00"), DataFim = DateTime.Parse("2020-02-01 15:00:00"), LugarID = 1 });


            dbContext.SaveChanges();
        }
    }
}
