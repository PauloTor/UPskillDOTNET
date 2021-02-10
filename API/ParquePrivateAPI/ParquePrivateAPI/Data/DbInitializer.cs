using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ParquePrivateAPI.Models;

 
namespace ParquePrivateAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ParquePrivateAPIContext context)
        {
            //context.Database.EnsureCreated();

            //Look for any Doencas.
            if (context.Reserva.Any())
            {
                return;   // DB has been seeded
            }


            var morada = new Morada[]

                {
                    new Morada
                    {

                        Rua = " Rua da Boavista",
                        CodigoPostal = "5002 - 456"
                    },
                };


            foreach (Morada s in morada)
            {
                context.Morada.Add(s);
            }
            context.SaveChanges();

           // ===============================================================
            var parque = new Parque[]

                {


            new Parque { NifParque =111112222,NomeParque = " Boavista Park", Lotacao = 5, MoradaID = 1 },
             };

            foreach (Parque s in parque)
            {
                context.Parque.Add(s);
            }
            context.SaveChanges();

            //=================================================================


            var lugar = new Lugar[]

                {

                    new Lugar { Fila = 1, Sector = 1, Preço = 10, ParqueID = 1 },
                    new Lugar { Fila = 2, Sector = 2, Preço = 11, ParqueID = 1 },
                    new Lugar { Fila = 3, Sector = 1, Preço = 10, ParqueID = 1 },
                    new Lugar { Fila = 4, Sector = 2, Preço = 11, ParqueID = 1 },
                    new Lugar { Fila = 5, Sector = 1, Preço = 10, ParqueID = 1 },
                };

            foreach (Lugar s in lugar)
            {
                context.Lugar.Add(s);
            }
            context.SaveChanges();
//===========================================================================================0

            var reserva = new Reserva[]
{
            new Reserva
            {
                DataReserva = DateTime.Parse("2020-01-01 15:00:00"),
                DataInicio = DateTime.Parse("2020-01-05 15:00:00"),
                DataFim = DateTime.Parse("2020-01-05 16:00:00"),
                LugarID = 1
            },

            new Reserva
            {
                DataReserva = DateTime.Parse("2020-01-2 15:00:00"),
                DataInicio = DateTime.Parse("2020-01-04 11:00:00"),
                DataFim = DateTime.Parse("2020-01-04 13:00:00"),
                LugarID = 2
            },

            new Reserva
            { 
                DataReserva = DateTime.Parse("2019-12-15 15:00:00"),
                DataInicio = DateTime.Parse("2020-02-09 17:00:00"),
                DataFim = DateTime.Parse("2020-02-09 18:00:00"),
                LugarID = 3
            },

            new Reserva
            {
                DataReserva = DateTime.Parse("2019-12-10 10:00:00"),
                DataInicio = DateTime.Parse("2020-02-01 11:00:00"),
                DataFim = DateTime.Parse("2020-02-01 15:00:00"),
                LugarID = 1
            }
        };
            foreach (Reserva s in reserva)
            {
                context.Reserva.Add(s);
            }
            context.SaveChanges();
      
        }


    }
}






















