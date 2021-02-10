using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ParquePrivateAPI.Data;
using ParquePrivateAPI.Models;

namespace testProject
{
    class ParquePrivateAPIContextMocker
    {
        private static ParquePrivateAPIContext dbContext;
        public static ParquePrivateAPIContext GetParquePrivateAPIContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ParquePrivateAPIContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;
            dbContext = new ParquePrivateAPIContext(options);
            Seed();
            return dbContext;
        }

        public static void Seed()
        {
            dbContext.Morada.Add(new Morada { MoradaID = 1, Rua = "Boavista", CodigoPostal = "4050-102" });
            dbContext.Morada.Add(new Morada { MoradaID = 2, Rua = "Avenida dos Aliados", CodigoPostal = "4050-150" });
            dbContext.Parque.Add(new Parque { NIFParqueID = 1, NomeParque = "Boavista Park", Lotacao = 5, MoradaID = 1});
            dbContext.Parque.Add(new Parque { NIFParqueID = 2, NomeParque = "Avenida dos Aliados Park", Lotacao = 5, MoradaID = 2});
            dbContext.Lugar.Add(new Lugar { LugarID = 1, Fila = 1, Sector = 1 , Preço = 5, NIFParqueID = 1});
            dbContext.Lugar.Add(new Lugar { LugarID = 2, Fila = 2, Sector = 1, Preço = 6, NIFParqueID = 1 });
            dbContext.Lugar.Add(new Lugar { LugarID = 3, Fila = 1, Sector = 2, Preço = 7, NIFParqueID = 1 });
            dbContext.Lugar.Add(new Lugar { LugarID = 4, Fila = 2, Sector = 1, Preço = 8, NIFParqueID = 1 });
            dbContext.Lugar.Add(new Lugar { LugarID = 5, Fila = 1, Sector = 1, Preço = 9, NIFParqueID = 1 });
            dbContext.Lugar.Add(new Lugar { LugarID = 6, Fila = 2, Sector = 1, Preço = 5, NIFParqueID = 2 });
            dbContext.Lugar.Add(new Lugar { LugarID = 7, Fila = 2, Sector = 2, Preço = 6, NIFParqueID = 2 });
            dbContext.Lugar.Add(new Lugar { LugarID = 8, Fila = 2, Sector = 1, Preço = 7, NIFParqueID = 2 });
            dbContext.Lugar.Add(new Lugar { LugarID = 9, Fila = 2, Sector = 2, Preço = 8, NIFParqueID = 2 });
            dbContext.Lugar.Add(new Lugar { LugarID = 10, Fila = 2, Sector = 1, Preço = 9, NIFParqueID = 2 });
            dbContext.Reserva.Add(new Reserva { ReservaID = 1, DataReserva = DateTime.Parse("2020-01-01 15:00:00"), DataInicio = DateTime.Parse("2020-01-05 15:00:00"), DataFim = DateTime.Parse("2020-01-05 16:00:00"),  LugarID = 1});
            dbContext.Reserva.Add(new Reserva { ReservaID = 2, DataReserva = DateTime.Parse("2020-01-2 15:00:00"), DataInicio = DateTime.Parse("2020-01-04 11:00:00"), DataFim = DateTime.Parse("2020-01-04 13:00:00"), LugarID = 2});
            dbContext.Reserva.Add(new Reserva { ReservaID = 3, DataReserva = DateTime.Parse("2019-12-15 15:00:00"), DataInicio = DateTime.Parse("2020-02-09 17:00:00"), DataFim = DateTime.Parse("2020-02-09 18:00:00"), LugarID = 3});
            dbContext.Reserva.Add(new Reserva { ReservaID = 4, DataReserva = DateTime.Parse("2019-12-10 10:00:00"), DataInicio = DateTime.Parse("2020-02-01 11:00:00"), DataFim = DateTime.Parse("2020-02-01 15:00:00"), LugarID = 1});

            dbContext.SaveChanges();
        }
    }
}
