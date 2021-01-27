using ParquePublicoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParquePublicoAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(ParquePublicoAPIContext context)
        {
            //context.Database.EnsureCreated();

            //Look for any Doencas.
            if (context.Reserva.Any())
            {
                return;   // DB has been seeded
            }

            var lugar = new Lugar[]

                {
                    new Lugar { LugarID = 1, PrecoLugar = 2.00f, RuaID= 1 },
                    new Lugar { LugarID = 2, PrecoLugar = 3.10f, RuaID= 2 },
                    new Lugar { LugarID = 3, PrecoLugar = 2.00f, RuaID= 3 },
                    new Lugar { LugarID = 4, PrecoLugar = 1.00f, RuaID= 4 },
                    new Lugar { LugarID = 5, PrecoLugar = 2.00f, RuaID= 8 },
                    new Lugar { LugarID = 6, PrecoLugar = 2.00f, RuaID= 5 },
                    new Lugar { LugarID = 7, PrecoLugar = 1.50f, RuaID= 6 },
                    new Lugar { LugarID = 8, PrecoLugar = 4.00f, RuaID= 9 },
                    new Lugar { LugarID = 9, PrecoLugar = 2.50f, RuaID= 7 }
                };

            foreach (Lugar s in lugar)
            {
                context.Lugar.Add(s);
            }
            context.SaveChanges();

            var rua = new Rua[]

                {
                    new Rua { NomeRua = "Primeira", CodigoPostal = "1111 - 111", Lotacao = 11 },
                    new Rua { NomeRua = "Segunda", CodigoPostal = "2222 - 222", Lotacao = 22 },
                    new Rua { NomeRua = "Terceira", CodigoPostal = "3333 - 333", Lotacao = 33 },
                    new Rua { NomeRua = "Quarta", CodigoPostal = "4444 - 444", Lotacao = 44 },
                    new Rua { NomeRua = "Quinta", CodigoPostal = "5555 - 555", Lotacao = 55 },
                    new Rua { NomeRua = "Sexta", CodigoPostal = "6666 - 666", Lotacao = 66 },
                    new Rua { NomeRua = "Setima", CodigoPostal = "7777 - 777", Lotacao = 77 },
                    new Rua { NomeRua = "Oitava", CodigoPostal = "8888 - 888", Lotacao = 88 },
                    new Rua { NomeRua = "Nona", CodigoPostal = "9999 - 999", Lotacao = 99 }
                };

            foreach (Rua r in rua)
            {
                context.Rua.Add(r);
            }
            context.SaveChanges();

            var reserva = new Reserva[]

               {
                   new Reserva { ReservaID = 1, DataReserva = DateTime.Parse("2020-1-26 15:00:00"), DataInicio = DateTime.Parse("2020-02-12 16:00:00"), DataFim = DateTime.Parse("2020-02-12 17:00:00"), LugarID = 1},
                   new Reserva { ReservaID = 2, DataReserva = DateTime.Parse("2020-1-26 16:00:00"), DataInicio = DateTime.Parse("2020-02-12 15:00:00"), DataFim = DateTime.Parse("2020-02-12 18:00:00"), LugarID = 2},
                   new Reserva { ReservaID = 3, DataReserva = DateTime.Parse("2020-1-27 09:00:00"), DataInicio = DateTime.Parse("2020-02-12 15:00:00"), DataFim = DateTime.Parse("2020-02-12 18:00:00"), LugarID = 1},
                   new Reserva { ReservaID = 4, DataReserva = DateTime.Parse("2020-1-27 10:00:00"), DataInicio = DateTime.Parse("2020-02-13 09:00:00"), DataFim = DateTime.Parse("2020-02-13 21:00:00"), LugarID = 1}
               };

            foreach (Reserva r in reserva)
            {
                context.Reserva.Add(r);
            }
            context.SaveChanges();


            //===========================================================================================0
        }
    }
}
