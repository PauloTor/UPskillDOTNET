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
            //===========================================================================================0
        }
    }
}
